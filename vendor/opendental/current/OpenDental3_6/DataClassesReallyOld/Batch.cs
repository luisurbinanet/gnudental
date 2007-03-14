/*using System;

namespace OpenDental{
	
	///<summary>Used to send batch SQL Select statements.</summary>
	///<remarks>this is a first attempt at batch commands and it needs some refining.  The huge advantage is that it only involves ONE round trip to the database.</remarks>
	public class Batch:DataClass{
		/// <summary>Retrieves a set of tables from the database.</summary>
		/// <param name="tableList">A simple string with the names of each table separated by commas.</param>
		/// <remarks>Would like to eliminate the switch statement from this class and replace it with a more flexible strategy possibly using reflection.</remarks>
		public static void Select(string tableList){
			AssembleCommand(tableList);
			FillDataSet();
			AssignTableNames(tableList);
			RefreshClasses(tableList);
		}

		private static void AssembleCommand(string tableList){
			string[] tableArray=tableList.Split(',');
			cmd.CommandText="";
			for(int i=0;i<tableArray.Length;i++){
				switch(tableArray[i]){
					case "graphicassembly":
						cmd.CommandText+=GraphicAssemblies.GetSelectText();
						break;
					case "graphicelement":
						cmd.CommandText+=GraphicElements.GetSelectText();
						break;
					case "graphicshape":
						cmd.CommandText+=GraphicShapes.GetSelectText();
						break;
					case "graphictype":
						cmd.CommandText+=GraphicTypes.GetSelectText();
						break;
				}
			}
		}

		private static void AssignTableNames(string tableList){
			string[] tableArray=tableList.Split(',');
			for(int i=0;i<tableArray.Length;i++){
				switch(tableArray[i]){
					default:
						ds.Tables[i].TableName=tableArray[i];
						break;
					//only reason to not use default is if you use parameters(no examples of that yet)
				}
			}
		}

		private static void RefreshClasses(string tableList){
			string[] tableArray=tableList.Split(',');
			for(int i=0;i<tableArray.Length;i++){
				switch(tableArray[i]){
					case "graphicassembly":
						GraphicAssemblies.Refresh();
						break;
					case "graphicelement":
						GraphicElements.Refresh();
						break;
					case "graphicshape":
						GraphicShapes.Refresh();
						break;
					case "graphictype":
						GraphicTypes.Refresh();
						break;
				}
			}//for
		}



	}
}*/