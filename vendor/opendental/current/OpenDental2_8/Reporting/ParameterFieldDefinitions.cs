using System;
using System.Collections;

namespace OpenDental.Reporting
{
	///<summary>Strongly typed collection of type ParameterFieldDefinition.</summary>
	public class ParameterFieldDefinitions:CollectionBase{

		///<summary>Returns the ParameterField with the given index.</summary>
		public ParameterFieldDefinition this[int index]{
      get{
				return((ParameterFieldDefinition)List[index]);
      }
      set{
				List[index]=value;
      }
		}

		///<summary>Returns the ParameterField with the given name.</summary>
		public ParameterFieldDefinition this[string name]{
			get{
				foreach(ParameterFieldDefinition pf in List){
					if(pf.Name==name)
						return pf;
				}
				return null;
      }
		}

		///<summary></summary>
		public int Add(ParameterFieldDefinition value){
			return(List.Add(value));
		}

		///<summary></summary>
		public int IndexOf(ParameterFieldDefinition value){
			return(List.IndexOf(value));
		}

		///<summary></summary>
		public void Insert(int index,ParameterFieldDefinition value){
			List.Insert(index,value);
		}

		
		//public int GetIndexOfType(SectionType sectType){
			
		//	return -1;
		//}


	}

}
