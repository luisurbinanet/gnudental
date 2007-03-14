using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SparksToothChart {
	///<summary>For 3D tooth graphics, this is a group of faces within a single tooth.  Different groups can be assigned different colors and visibility.  Groups might include enamel, the filling surfaces, pulp, canals, and cementum.  If a group does not apply, such as a F on a posterior tooth, then that tooth will not have that group.  The code must be resiliant enough to handle missing groups.  We might add more groups later and subdivide existing groups.  For instance pits, grooves, cusps, cervical areas, etc.  Over the years, this could get quite extensive and complex.  We would have to add a table to the database to handle additional groups painted by user.</summary>
	class ToothGroup {
		///<summary></summary>
		public bool Visible;
		///<summary></summary>
		public Color PaintColor;
		///<summary></summary>
		public ToothGroupType GroupType;
		///<summary>dim 1=the face. dim 2=the vertex. dim 3 always has length=2, with 1st vertex, and 2nd normal.</summary>
		public int[][][] Faces;


	}

	///<summary></summary>
	public enum ToothGroupType{
		///<summary></summary>
		Enamel,
		///<summary></summary>
		Cementum,
		///<summary></summary>
		M,
		///<summary></summary>
		O,
		///<summary></summary>
		D,
		///<summary></summary>
		B,
		///<summary></summary>
		L,
		///<summary></summary>
		F,
		///<summary></summary>
		I,
		///<summary>class V. In addition to B or F</summary>
		V,
		///<summary>Only present in the special implant tooth object.</summary>
		Implant,
		///<summary>Just a placeholder. The pulp chamber and post or buildup.</summary>
		Buildup,
		///<summary>Not used. Just a placeholder</summary>
		Canals
	}
}
