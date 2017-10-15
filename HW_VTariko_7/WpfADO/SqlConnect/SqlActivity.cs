using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Runtime.CompilerServices;
using WpfADO.Annotations;

namespace WpfADO.SqlConnect
{
	public class SqlActivity
	{
		#region Константы

		private const string CONNECTION_STRING =
				@"Integrated Security=SSPI;
				Persist Security Info=False;
				Initial Catalog=Company;
				Data Source=DESKTOP-TE25SGR\SQLEXPRESS";

		#endregion

		#region Поля

		private SqlConnection _connection = new SqlConnection();
		private SqlDataAdapter _adapter;
		private DataTable _dataTable;

		#endregion

		#region Свойства

		public DataTable Table => _dataTable;

		public Dictionary<string, string> Departments { get; private set; }


		#endregion

		#region Конструктор

		public SqlActivity()
		{
			InitAdapter();
		}

		#endregion

		#region Методы

		/// <summary>
		/// Иницилизация связи с БД
		/// </summary>
		/// <returns></returns>
		public void InitAdapter()
		{
			_connection = CreateConnection();
			_adapter = new SqlDataAdapter();

			// Select
			SqlCommand sqlCommand = new SqlCommand(@"SELECT emp.EmployeeId AS ID,
														emp.LastName AS LastName,
														emp.FirstName AS FirstName,
														emp.BirthDate AS BirthDate,
														(CONVERT(int, CONVERT(char(8),GETDATE(),112)) - CONVERT(char(8),emp.BirthDate,112)) / 10000 AS Age,
														d.DepartmentName AS Department,
														d.Code AS DepartmentCode
														FROM Company.HR.Employees  AS emp
														JOIN Company.HR.Department AS d
														ON d.DepartmentId = emp.DepartmentId", _connection);

			_adapter.SelectCommand = sqlCommand;

			// Insert
			sqlCommand = new SqlCommand(@"INSERT INTO HR.Employees(LastName, FirstName, DepartmentId, BirthDate)
											SELECT LTRIM(RTRIM(@LastName)), LTRIM(RTRIM(@FirstName)), d.DepartmentId, @BirthDate
												FROM [HR].[Department] AS d
												WHERE d.Code = LTRIM(RTRIM(@DepartmentCode));
										SET @ID = @@IDENTITY;
										SET @Department = (SELECT DepartmentName FROM HR.Department WHERE Code = LTRIM(RTRIM(@DepartmentCode)))",
										_connection);

			sqlCommand.Parameters.Add("@LastName", SqlDbType.NChar, 15, "LastName");
			sqlCommand.Parameters.Add("@FirstName", SqlDbType.NChar, 10, "FirstName");
			sqlCommand.Parameters.Add("@BirthDate", SqlDbType.Date, 0, "BirthDate");
			sqlCommand.Parameters.Add("@DepartmentCode", SqlDbType.NChar, 10, "DepartmentCode");
			SqlParameter paramId = sqlCommand.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
			paramId.Direction = ParameterDirection.Output;
			SqlParameter paramDepartment = sqlCommand.Parameters.Add("@Department", SqlDbType.NChar, 50, "Department");
			paramDepartment.Direction = ParameterDirection.Output;
			_adapter.InsertCommand = sqlCommand;

			// Update
			sqlCommand = new SqlCommand(@"UPDATE HR.Employees
											SET LastName = LTRIM(RTRIM(@LastName)),
											FirstName = LTRIM(RTRIM(@Firstname)),
											BirthDate = @BirthDate,
											DepartmentId = (SELECT DepartmentId FROM HR.Department WHERE Code = LTRIM(RTRIM(@DepartmentCode)))
											WHERE EmployeeId = @ID;
											SET @Department = (SELECT DepartmentName FROM HR.Department WHERE Code = LTRIM(RTRIM(@DepartmentCode)))",
											_connection);

			sqlCommand.Parameters.Add("@LastName", SqlDbType.NChar, 15, "LastName");
			sqlCommand.Parameters.Add("@FirstName", SqlDbType.NChar, 10, "FirstName");
			sqlCommand.Parameters.Add("@BirthDate", SqlDbType.Date, 0, "BirthDate");
			sqlCommand.Parameters.Add("@DepartmentCode", SqlDbType.NChar, 10, "DepartmentCode");
			paramId = sqlCommand.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
			paramId.SourceVersion = DataRowVersion.Original;
			paramDepartment = sqlCommand.Parameters.Add("@Department", SqlDbType.NChar, 50, "Department");
			paramDepartment.Direction = ParameterDirection.Output;
			_adapter.UpdateCommand = sqlCommand;

			// Delete
			sqlCommand = new SqlCommand("DELETE FROM HR.Employees WHERE EmployeeId = @ID", _connection);
			paramId = sqlCommand.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
			paramId.SourceVersion = DataRowVersion.Original;
			_adapter.DeleteCommand = sqlCommand;

			_dataTable = new DataTable();
			_adapter.Fill(Table);
			Departments = new Dictionary<string, string>();
			foreach (DataRow row in Table.Rows)
			{
				string depCode = row["DepartmentCode"].ToString();
				string depName = row["Department"].ToString();
				if (!Departments.ContainsKey(depCode))
				{
					Departments.Add(depCode, depName);
				}
			}
		}

		/// <summary>
		/// Возвращает новую строку для таблицы
		/// </summary>
		/// <returns></returns>
		public DataRow NewRow()
		{
			return Table.NewRow();
		}

		/// <summary>
		/// Обработка доабвленяи новой строки
		/// </summary>
		/// <param name="dataRow">Добавляемая строка</param>
		public void AddRow(DataRow dataRow)
		{
			Table.Rows.Add(dataRow);
			AdapterUpdate();
		}

		/// <summary>
		/// Обновление адаптера
		/// </summary>
		public void AdapterUpdate()
		{
			_adapter.Update(Table);
		}

		#region Соединение с БД

		/// <summary>
		/// Создание соединения с БД
		/// </summary>
		/// <returns></returns>
		private SqlConnection CreateConnection()
		{
			SqlConnection connection =
				new SqlConnection { ConnectionString = CONNECTION_STRING };
			try
			{
				connection.Open();
			}
			catch (Exception ex)
			{
				throw new Exception("Ошибка соединения с БД", ex);
			}

			return connection;
		}

		/// <summary>
		/// Закрытие соединения с БД
		/// </summary>
		public void ConnectionClose()
		{
			if (_connection.State == ConnectionState.Open)
				_connection.Close();
		} 
		#endregion

		#endregion
	}
}
