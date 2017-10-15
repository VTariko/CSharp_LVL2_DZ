using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfADO.SqlConnect;

namespace WpfADO
{
	static class Logic
	{
		public static void MButtonAdd(SqlActivity activity)
		{
			DataRow dataRow = activity.NewRow();
			EditWindow edit = new EditWindow(dataRow, activity.Departments);
			edit.ShowDialog();
			if (edit.DialogResult.HasValue && edit.DialogResult.Value)
			{
				activity.AddRow(edit.ResultRow);
			}
		}

		public static void MButtonEdit(SqlActivity activity, object row)
		{
			DataRowView newRow = (DataRowView) row;
			newRow.BeginEdit();
			EditWindow edit = new EditWindow(newRow.Row, activity.Departments);
			edit.ShowDialog();
			if (edit.DialogResult.HasValue && edit.DialogResult.Value)
			{
				newRow.EndEdit();
				activity.AdapterUpdate();
			}
		}

		public static void MButtonDel(SqlActivity activity, object row)
		{
			DataRowView newRow = (DataRowView) row;
			newRow.Row.Delete();
			activity.AdapterUpdate();
		}

		public static void EButtonOk(EditWindow edit, Dictionary<string, string> deps)
		{
			edit.ResultRow["LastName"] = edit.txtLastName.Text.Trim(); ;
			edit.ResultRow["FirstName"] = edit.txtFirstName.Text.Trim(); ;
			edit.ResultRow["BirthDate"] = edit.dpAge.SelectedDate;
			string dep = (string) edit.boxDepartments.SelectedItem;
			edit.ResultRow["DepartmentCode"] = deps.FirstOrDefault(d => d.Value == dep).Key.Trim();
			edit.DialogResult = true;
		}
	}
}
