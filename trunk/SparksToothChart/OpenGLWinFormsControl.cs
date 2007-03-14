/* File: OpenGLWinFormsControl.cs
 * Description: A Windows Forms control that supports OpenGL rendering using the Tao library.
 * Author: Michael Hansen (hansen.mike@gmail.com)
 * Date Created: 3/2/2006
 * Date Modified: 3/30/2006
 * 
 * Overview
 * ========
 * This control provides a simple Windows Forms control to do OpenGL render using Tao. The control
 * behaves properly in design mode (while being designed and while embedded in another control that's
 * being designed).
 * 
 * Example Usage
 * =============
 * To use this control, add it to your Form or Control using the designer or programmatically.
 * In your initialization routine, you MUST set TaoRenderEnabled to true for the control to use
 * OpenGL rendering (this is initially set to false to allow for designing).
 * 
 * For the most basic usage, add event handlers to the TaoSetupContext and TaoRenderScene events.
 * In TaoSetupContext, you will do your normal OpenGL setup routine.
 * In TaoRenderScene, you will do the actual drawing (by default, glFinish will be called for you after rendering).
 * During your initialization routine, call TaoInitializeContexts; this will create the device
 * and rendering contexts as well as call your TaoSetupContext event handler.
 * During each frame, call TaoDraw to redraw the scene. This will call Invalidate on the control and
 * call your TaoRenderScene event handler.
 * 
 * The step-by-step usage of the control would then be this:
 * 1. Add event handlers to TaoSetupContext (OpenGL initialization) and TaoRenderScene (rendering)
 * 2. Call TaoInitializeContexts
 * 3. Set TaoRenderEnabled to true
 * 4. Call TaoDraw during each frame
 * 
 * Advanced Usage
 * ==============
 * For more advanced usage, you may set the number of bits for the accumulator (TaoAccumBits),
 * color depth (TaoColorBits), depth buffer (TaoDepthBits), and stencil buffer (TaoStencilBits)
 * before calling TaoInitializeContexts.
 * 
 * Add an event handler to TaoControlSizeChanged to be notified whenever the control resizes.
 * The event arguments will give you the control's new width and height.  Adding a handler to this
 * event will disable the default resizing behavior of the control (reset the viewport and redraw).
 * 
 * Add an event handler to TaoOpenGLError to receive notifications of any errors that occur during
 * rendering. The event arguments will give you the error code and a brief description of the
 * error that occurred.
 */
/* 
 * Modified by Frederik Carlier: Patch to run on Linux.
 * Patch Copyright (c) 2007 Frederik Carlier
 */

#region Imported Namespaces

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security;

using Tao.OpenGl;
using Tao.Platform.Windows;

#endregion

namespace Tao.Platform.Windows.Controls {
	/// <summary>
	/// A Windows Forms control that supports OpenGL rendering using the Tao library.
	/// </summary>
	public class OpenGLWinFormsControl:Control {
		#region Protected Fields

		protected IntPtr deviceContext = IntPtr.Zero,renderContext = IntPtr.Zero;
		protected bool renderEnabled = false;

		protected bool autoMakeCurrent = true,autoSwapBuffers = false,autoFinish = false;

		protected byte accumBits = 0,colorBits = 32,depthBits = 16,stencilBits = 0;
		protected int lastErrorCode = Gl.GL_NO_ERROR;

		#endregion

		#region Events

		/// <summary>
		/// A user-defined event that renders the scene (called during each redraw).
		/// </summary>
		public event EventHandler TaoRenderScene;

		/// <summary>
		/// A user-defined event that sets up the OpenGL context (called once during TaoInitializeContexts).
		/// </summary>
		public event EventHandler TaoSetupContext;

		/// <summary>
		/// A user-defined event that's called when the control resizes
		/// (by default, the control resets the viewport and redraws itself).
		/// </summary>
		public event EventHandler<SizeChangedEventArgs> TaoControlSizeChanged;

		/// <summary>
		/// Fired whenever an error occurs during rendering.
		/// </summary>
		public event EventHandler<OpenGLErrorEventArgs> TaoOpenGLError;

		#endregion

		#region Properties

		/// <summary>
		/// Enables / disables rendering. IMPORTANT: This property is initially set to false to allow for smooth designing.
		/// You MUST set this to true before any rendering will take place.
		/// </summary>
		public bool TaoRenderEnabled {
			get {
				return (renderEnabled);
			}
			set {
				renderEnabled = value;
			}
		}

		/// <summary>
		/// True if both the device and rendering contexts have been created
		/// </summary>
		protected bool ContextsReady {
			get {
				return ((deviceContext != IntPtr.Zero) && (renderContext != IntPtr.Zero));
			}
		}

		#endregion

		public OpenGLWinFormsControl() {
			//Setup the control's styles
			SetStyle( ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque |
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint,true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer,false);//Disable C# double buffering.
			this.DoubleBuffered = false;												//so that it does not interfere with OpenGL.

			//Set default size
			this.Size = new Size(100,100);
		}

		#region Protected Methods
		#region int _DescribePixelFormat(System.IntPtr hdc,int iPixelFormat,uint nBytes,ref Gdi.PIXELFORMATDESCRIPTOR ppfd)
		/// <summary>
		/// 
		/// </summary>
		[DllImport("gdi32.dll",EntryPoint="DescribePixelFormat",SetLastError=true),SuppressUnmanagedCodeSecurity]
		public static extern int _DescribePixelFormat(System.IntPtr hdc,int iPixelFormat,uint nBytes,ref Gdi.PIXELFORMATDESCRIPTOR ppfd);
		#endregion int _DescribePixelFormat(System.IntPtr hdc,int iPixelFormat,uint nBytes,ref Gdi.PIXELFORMATDESCRIPTOR ppfd)


		#region int DescribePixelFormat(HDC  hdc,int  iPixelFormat,UINT  nBytes,LPPIXELFORMATDESCRIPTOR  ppfd)
		protected int DescribePixelFormat(System.IntPtr hdc,int iPixelFormat,uint nBytes,ref Gdi.PIXELFORMATDESCRIPTOR ppfd) {
			Kernel.LoadLibrary("opengl32.dll");
			return _DescribePixelFormat(hdc,iPixelFormat,nBytes,ref ppfd);
		}
		#endregion int DescribePixelFormat(HDC  hdc,int  iPixelFormat,UINT  nBytes,LPPIXELFORMATDESCRIPTOR  ppfd)

		///<summary> 
		///bpp: -1 is "don't care", positive otherwise
		///depth: -1 is "don't care", positive otherwise
		///dbl:-1 is "don't care", 0 for none, otherwise 1
		///acc:-1 is "don't care", 0 for none, otherwise 1
		///</summary>
		Gdi.PIXELFORMATDESCRIPTOR ChoosePixelFormatEx(System.IntPtr hdc,int p_bpp,int p_depth,int p_dbl,int p_acc) {
			int wbpp=p_bpp;
			int wdepth=p_depth;
			int wdbl=p_dbl;
			int wacc=p_acc;
			Gdi.PIXELFORMATDESCRIPTOR pfd = new Gdi.PIXELFORMATDESCRIPTOR();
			pfd.nSize = (short)Marshal.SizeOf(pfd);
			pfd.nVersion = 1;
			int num=1;
			num=this.DescribePixelFormat(hdc,num,(uint)pfd.nSize,ref pfd);
			uint maxqual=0;
			int maxindex=0;
			if(num>0) {
				int max_bpp;
				int max_depth;
				int max_dbl;
				int max_acc;
				for(int i=1;i<=num;i++) {
					pfd=new Gdi.PIXELFORMATDESCRIPTOR();
					pfd.nSize=(short)Marshal.SizeOf(pfd);
					pfd.nVersion=1;
					this.DescribePixelFormat(hdc,i,(uint)pfd.nSize,ref pfd);
					int bpp=pfd.cColorBits;
					int depth=pfd.cDepthBits;
					bool pal=(pfd.iPixelType==Gdi.PFD_TYPE_COLORINDEX);
					bool mcd=(pfd.dwFlags & Gdi.PFD_GENERIC_FORMAT)!=0 && (pfd.dwFlags & Gdi.PFD_GENERIC_ACCELERATED)!=0;
					bool soft=(pfd.dwFlags & Gdi.PFD_GENERIC_FORMAT)!=0 && (pfd.dwFlags & Gdi.PFD_GENERIC_ACCELERATED)==0;
					bool icd=(pfd.dwFlags & Gdi.PFD_GENERIC_FORMAT)==0 && (pfd.dwFlags & Gdi.PFD_GENERIC_ACCELERATED)==0;
					bool opengl=(pfd.dwFlags & Gdi.PFD_SUPPORT_OPENGL)!=0;
					bool window=(pfd.dwFlags & Gdi.PFD_DRAW_TO_WINDOW)!=0;
					bool bitmap=(pfd.dwFlags & Gdi.PFD_DRAW_TO_BITMAP)!=0;
					bool dbuff=(pfd.dwFlags & Gdi.PFD_DOUBLEBUFFER)!=0;
					uint q=0;
					if(opengl && window) {
						q=q+0x8000;
					}
					if(wdepth==-1 || (wdepth>0 && depth>0)) {
						q=q+0x4000;
					}
					if(wdbl==-1 || (wdbl==0 && !dbuff) || (wdbl==1 && dbuff)) {
						q=q+0x2000;
					}
					if(wacc==-1 || (wacc==0 && soft) || (wacc==1 && (mcd || icd))) {
						q=q+0x1000;
					}
					if(mcd || icd) {
						q=q+0x0040;
					}
					if(icd) {
						q=q+0x0002;
					}
					if(wbpp==-1 || (wbpp==bpp)) {
						q=q+0x0800;
					}
					if(bpp>=16) {
						q=q+0x0020;
					}
					if(bpp==16) {
						q=q+0x0008;
					}
					if(wdepth==-1 || (wdepth==depth)) {
						q=q+0x0400;
					}
					if(depth>=16) {
						q=q+0x0010;
					}
					if(depth==16) {
						q=q+0x0004;
					}
					if(!pal) {
						q=q+0x0080;
					}
					if(bitmap) {
						q=q+0x0001;
					}
					if(q>maxqual) {
						maxqual=q;
						maxindex=i;
						max_bpp=bpp;
						max_depth=depth;
						max_dbl=dbuff?1:0;
						max_acc=soft?0:1;
					}
				}
			}
			else {
				MessageBox.Show(this.ToString()+".ChoosePixelFormatEx: there are no valid rendering contexts for this application!");
			}
			pfd=new Gdi.PIXELFORMATDESCRIPTOR();
			pfd.nSize=(short)Marshal.SizeOf(pfd);
			pfd.nVersion=1;
			this.DescribePixelFormat(hdc,maxindex,(uint)pfd.nSize,ref pfd);
			//MessageBox.Show(maxindex.ToString());
			return pfd;
		}

		/// <summary>
		/// Creates the device and rendering contexts using the supplied settings
		/// in accumBits, colorBits, depthBits, and stencilBits.
		/// </summary>
		protected virtual void CreateContexts() {
#if MONO
            return;
#warning "OpenGL support is disabled."
#endif
			//Make sure the handle for this control has been created
			if(this.Handle == IntPtr.Zero) {
				throw new Exception("CreateContexts: The control's window handle has not been created.");
			}
			/*			bool useHardware=true;//Set true to enable hardware mode. Set to false to enable software rendering mode.
						//Setup pixel format
						Gdi.PIXELFORMATDESCRIPTOR pixelFormat = new Gdi.PIXELFORMATDESCRIPTOR();
						pixelFormat.nSize = (short)Marshal.SizeOf(pixelFormat);
						pixelFormat.nVersion = 1;
						pixelFormat.dwFlags = Gdi.PFD_DRAW_TO_WINDOW |	
																	Gdi.PFD_SUPPORT_OPENGL |		//OpenGL allowed.
																	Gdi.PFD_DOUBLEBUFFER;				//Want double buffering.
						if(useHardware){
							pixelFormat.dwFlags|=Gdi.PFD_GENERIC_ACCELERATED;
							pixelFormat.dwFlags&=~Gdi.PFD_GENERIC_FORMAT;
						}else{
							pixelFormat.dwFlags|=Gdi.PFD_GENERIC_FORMAT;
							pixelFormat.dwFlags&=~Gdi.PFD_GENERIC_ACCELERATED;
						}
						pixelFormat.iPixelType = (byte)Gdi.PFD_TYPE_RGBA;
						pixelFormat.cColorBits = colorBits;
						pixelFormat.cRedBits = 0;
						pixelFormat.cRedShift = 0;
						pixelFormat.cGreenBits = 0;
						pixelFormat.cGreenShift = 0;
						pixelFormat.cBlueBits = 0;
						pixelFormat.cBlueShift = 0;
						pixelFormat.cAlphaBits = 0;
						pixelFormat.cAlphaShift = 0;
						pixelFormat.cAccumBits = accumBits;
						pixelFormat.cAccumRedBits = 0;
						pixelFormat.cAccumGreenBits = 0;
						pixelFormat.cAccumBlueBits = 0;
						pixelFormat.cAccumAlphaBits = 0;
						pixelFormat.cDepthBits = depthBits;
						pixelFormat.cStencilBits = stencilBits;
						pixelFormat.cAuxBuffers = 0;
						pixelFormat.iLayerType = (byte)Gdi.PFD_MAIN_PLANE;
						pixelFormat.bReserved = 0;
						pixelFormat.dwLayerMask = 0;
						pixelFormat.dwVisibleMask = 0;
						pixelFormat.dwDamageMask = 0;
			*/
			//Create device context
			deviceContext = User.GetDC(this.Handle);

			if(deviceContext == IntPtr.Zero) {
				throw new Exception("CreateContexts: Unable to create an OpenGL device context");
			}

			Gdi.PIXELFORMATDESCRIPTOR[] pfds=new Gdi.PIXELFORMATDESCRIPTOR[6];

			/*pfds[0]=ChoosePixelFormatEx(deviceContext,//The window context.
																	16,						//Bits-per-pixel of color
																	16,						//Z-depth
																	1,						//Use double buffering. 
																	0);						//Use software rendering.

			pfds[1]=ChoosePixelFormatEx(deviceContext,//The window context.
																	16,						//Bits-per-pixel of color
																	16,						//Z-depth
																	1,						//Use double buffering. 
																	1);						//Use hardware rendering.

			pfds[2]=ChoosePixelFormatEx(deviceContext,//The window context.
																	16,						//Bits-per-pixel of color
																	16,						//Z-depth
																	0,						//Don't use double buffering. 
																	0);						//Use software rendering.

			pfds[3]=ChoosePixelFormatEx(deviceContext,//The window context.
																	16,						//Bits-per-pixel of color
																	16,						//Z-depth
																	0,						//Don't use double buffering. 
																	1);						//Use hardware rendering.

			pfds[4]=ChoosePixelFormatEx(deviceContext,//The window context.
																	24,						//Bits-per-pixel of color
																	24,						//Z-depth
																	0,						//Don't use double buffering. 
																	0);						//Use software rendering.*/

			pfds[5]=ChoosePixelFormatEx(deviceContext,//The window context.
																	8,						//Bits-per-pixel of color
																	8,						//Z-depth
																	autoSwapBuffers?1:0,//Don't use double buffering. 
																	1);						//Use hardware rendering.

			Gdi.PIXELFORMATDESCRIPTOR pixelFormat=pfds[5];

			//Set pixel format
			int selectedFormat = Gdi.ChoosePixelFormat(deviceContext,ref pixelFormat);

			//Make sure the requested pixel format is available
			if(selectedFormat == 0) {
				throw new Exception("CreateContexts: Unable to find a suitable pixel format");
			}

			if(!Gdi.SetPixelFormat(deviceContext,selectedFormat,ref pixelFormat)) {
				throw new Exception(string.Format("CreateContexts: Unable to set the requested pixel format ({0})",selectedFormat));
			}

			//Create rendering context
			renderContext = Wgl.wglCreateContext(deviceContext);

			if(renderContext == IntPtr.Zero) {
				throw new Exception("CreateContexts: Unable to create an OpenGL rendering context");
			}

			//Make this the current context
			MakeCurrentContext();
		}

		/// <summary>
		/// Deletes both the device and rendering contexts if they've been created.
		/// </summary>
		protected virtual void DisposeContext() {
			//Dispose of rendering context
			if(renderContext != IntPtr.Zero) {
				Wgl.wglMakeCurrent(deviceContext,renderContext);
				Wgl.wglDeleteContext(renderContext);
				renderContext = IntPtr.Zero;
			}

			//Dispose of device context
			if(deviceContext != IntPtr.Zero) {
				User.ReleaseDC(this.Handle,deviceContext);
				deviceContext = IntPtr.Zero;
			}
		}

		protected override void Dispose(bool disposing) {
			if(disposing) {
				DisposeContext();
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Sets this control's OpenGL context as the current context.
		/// </summary>
		public void MakeCurrentContext() {
			if(!Wgl.wglMakeCurrent(deviceContext,renderContext)) {
				throw new Exception("MakeCurrentContext: Unable to active this control's OpenGL rendering context");
			}
		}

		/// <summary>
		/// Draws the design-mode background for the control.
		/// By default, a message is displayed to inform the user that the control is in design mode
		/// and how they can switch to rendering mode.
		/// </summary>
		/// <param name="controlGraphics"></param>
		protected void DrawDesignBackground(Graphics controlGraphics) {
			controlGraphics.Clear(Color.White);

			//Draw heading string
			controlGraphics.DrawString("Tao OpenGL WinForms Control",
					new Font("Arial",14.0f,FontStyle.Bold),Brushes.Black,10.0f,10.0f);

			//Draw information string
			Font infoFont = new Font("Arial",12.0f);

			controlGraphics.DrawString("This control is currently in design mode.",
					infoFont,Brushes.Black,10.0f,35.0f);

			controlGraphics.DrawString("You must set TaoRenderEnabled to true for OpenGL rendering.",
					infoFont,Brushes.Black,10.0f,55.0f);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Creates device and rendering contexts then fires the user-defined SetupContext event
		/// (if the contexts have not already been created). Call this in your initialization routine.
		/// </summary>
		public void TaoInitializeContexts() {
			if(!ContextsReady) {
				CreateContexts();

				//Fire the user-defined TaoSetupContext event
				if(TaoSetupContext != null) {
					TaoSetupContext(this,null);
				}
			}
		}

		/// <summary>
		/// Call this method to redraw the control every frame (internally, this calls Invalidate)
		/// </summary>
		public void TaoDraw() {
			Invalidate();
		}

		#endregion

		#region Control Methods

		protected override void OnPaintBackground(PaintEventArgs pevent) {
			//Do not paint background
		}

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);

			//Only draw with OpenGL if rendering is enabled (disabled by default for designing)
			if(renderEnabled) {
				//Initialize the device and rendering contexts if the user hasn't already
				TaoInitializeContexts();

				//Make this the current context
				if(autoMakeCurrent) {
					//Only switch contexts if this is already not the current context
					if(renderContext != Wgl.wglGetCurrentContext()) {
						MakeCurrentContext();
					}
				}

				//Fire the user-defined TaoRenderScene event
				if(TaoRenderScene != null) {
					TaoRenderScene(this,null);
				}

				//Automatically finish the scene
				if(autoFinish) {
					Gl.glFinish();
				}

				//Automatically check for errors
				lastErrorCode = Gl.glGetError();

				if(lastErrorCode != Gl.GL_NO_ERROR) {
					//Fire the error handling event
					if(TaoOpenGLError != null) {
						TaoOpenGLError(this,new OpenGLErrorEventArgs(lastErrorCode));
					}
				}

				//Swap the OpenGL buffer to the display
				if(autoSwapBuffers) {
					Gdi.SwapBuffersFast(deviceContext);
				}
			}
			else {
				//Draw the background for this control when it's in design
				//mode (TaoRenderEnabled = false)
				DrawDesignBackground(e.Graphics);
			}
		}

		protected override void OnSizeChanged(EventArgs e) {
			base.OnSizeChanged(e);

			if(ContextsReady && renderEnabled) {
				//Fire the user-defined TaoControlSizeChanged event
				if(TaoControlSizeChanged != null) {
					TaoControlSizeChanged(this,new SizeChangedEventArgs(this.Size));
				}
				else {
					//By default, resize the viewport and request a re-draw
					Gl.glViewport(0,0,this.Width,this.Height);
					Invalidate();
				}
			}
		}

		#endregion
	}

	#region EventArgs Classes

	public class SizeChangedEventArgs:EventArgs {
		private Size newSize = new Size();

		/// <summary>
		/// The new size of the control that has been resized.
		/// </summary>
		public Size NewSize {
			get {
				return (newSize);
			}
		}

		public SizeChangedEventArgs(Size newSize) {
			this.newSize = newSize;
		}
	}

	public class OpenGLErrorEventArgs:EventArgs {
		private int errorCode = Gl.GL_NO_ERROR;
		private string description = "";

		/// <summary>
		/// A brief description of the error.
		/// </summary>
		public string Description {
			get {
				return (description);
			}
		}

		/// <summary>
		/// The OpenGL error code.
		/// </summary>
		public int ErrorCode {
			get {
				return (errorCode);
			}
		}

		public OpenGLErrorEventArgs(int errorCode) {
			this.errorCode = errorCode;

			switch(errorCode) {
				case Gl.GL_INVALID_ENUM:
					description = "GL_INVALID_ENUM - An unacceptable value has been specified for an enumerated argument.  The offending function has been ignored.";
					break;

				case Gl.GL_INVALID_VALUE:
					description = "GL_INVALID_VALUE - A numeric argument is out of range.  The offending function has been ignored.";
					break;

				case Gl.GL_INVALID_OPERATION:
					description = "GL_INVALID_OPERATION - The specified operation is not allowed in the current state.  The offending function has been ignored.";
					break;

				case Gl.GL_STACK_OVERFLOW:
					description = "GL_STACK_OVERFLOW - This function would cause a stack overflow.  The offending function has been ignored.";
					break;

				case Gl.GL_STACK_UNDERFLOW:
					description = "GL_STACK_UNDERFLOW - This function would cause a stack underflow.  The offending function has been ignored.";
					break;

				case Gl.GL_OUT_OF_MEMORY:
					description = "GL_OUT_OF_MEMORY - There is not enough memory left to execute the function.  The state of OpenGL has been left undefined.";
					break;

				default:
					description = "Unknown OpenGL Error.";
					break;
			}
		}
	}

	#endregion
}
