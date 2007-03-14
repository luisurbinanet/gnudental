using System;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDental.ReportingOld2
{
	///<summary></summary>
	public class ScriptEngine{
		///<summary></summary>
		public static string[] FormulaCode;

		///<summary></summary>
		public ScriptEngine(){
			
		}

		///<summary></summary>
		public static void RunScript(Assembly assembly, string entryPoint){
			if(assembly==null){
				MessageBox.Show("Assembly is null");
				return;
			}
			try{
				//Use reflection to call the static Main function
				Module[] mods = assembly.GetModules(false);
				Type[] types = mods[0].GetTypes();
				//loop through each class that was defined and look for the first occurrance of the entry point method
				foreach(Type type in types){
					MethodInfo mi=type.GetMethod(entryPoint, BindingFlags.Public | BindingFlags.Static);
					if(mi != null){
						if(mi.ReturnType.Name != "Int32"){
							//if the entry point method doesnt return an Int32, then return the error constant
							mi.Invoke(null, null);
							//return NoOrErrorRetValue;
						}
						else{
							//if the entry point method does return in Int32, then capture it and return it
							//return (int)
							mi.Invoke(null, null);
						}
					}
				}
			}
			catch{
				MessageBox.Show("Script invalid");			
			}
		}



	}

}
