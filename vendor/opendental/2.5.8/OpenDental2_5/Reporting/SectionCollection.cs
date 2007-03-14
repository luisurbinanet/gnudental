using System;
using System.Collections;

namespace OpenDental.Reporting
{
	///<summary>Strongly typed collection of Sections.</summary>
	public class SectionCollection:CollectionBase{

		///<summary>Returns the Section with the given index.</summary>
		public Section this[int index]{
      get{
				return((Section)List[index]);
      }
      set{
				List[index]=value;
      }
		}

		///<summary></summary>
		public int Add(Section value){
			return(List.Add(value));
		}

		///<summary></summary>
		public int IndexOf(Section value){
			return(List.IndexOf(value));
		}

		///<summary></summary>
		public void Insert(int index,Section value){
			List.Insert(index,value);
		}

		///<summary>Returns the first Section index of the given kind. Since only one of each kind is allowed, this will reliably return the section of interest.</summary>
		public int GetIndexOfKind(AreaSectionKind kind){
			foreach(Section section in List){
				if(section.Kind==kind)
					return IndexOf(section);
			}
			return -1;
		}

		///<summary>Returns the first Section of the given kind. Since only one of each kind is allowed, this will reliably return the section of interest.</summary>
		public Section GetOfKind(AreaSectionKind kind){
			foreach(Section section in List){
				if(section.Kind==kind)
					return section;
			}
			return null;
		}


	}

}
