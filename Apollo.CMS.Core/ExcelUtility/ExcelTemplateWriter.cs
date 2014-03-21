// --------------------------------------------------------------------------
// <copyright file="ExcelTemplateWriter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.10.10">Created</change>
// </history>

namespace Apollo.CMS.Core.ExcelUtility
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Apollo.CMS.Core.ExcelUtility.NPOIExtensions;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NPOI.SS.Util;

    /// <summary>
    /// Excel Template Writer.
    /// </summary>
    public class ExcelTemplateWriter
    {
        #region Attributes

        private const float DefaultRowHeight = 15;
        private const string BreakLine = "\n";

        private string _fileName;
        private string _sheetName;
        private ISheet _templateSheet;
        private ISheet _sheet;
        private int _rowId = 0;

        #endregion Attributes

        #region Constructors

        public ExcelTemplateWriter(string filePath, string excelFileName = "Report.xls", string sheetName = "Result")
            : this(new FileStream(filePath, FileMode.Open, FileAccess.Read), excelFileName, sheetName)
        {
        }

        public ExcelTemplateWriter(FileStream fs, string excelFileName = "Report.xls", string sheetName = "Result")
            : this(new HSSFWorkbook(fs), excelFileName, sheetName)
        {
            fs.Close();
        }

        public ExcelTemplateWriter(HSSFWorkbook template, string excelFileName, string sheetName)
        {
            this._fileName = excelFileName;
            this._sheetName = sheetName;
            this._templateSheet = template.GetSheetAt(0);
            this._sheet = template.CreateSheet(this._sheetName);
        }

        #endregion Constructors

        #region Properties

        public int RowId
        {
            get
            {
                return this._rowId;
            }
        }

        #endregion Properties

        #region Export to stream

        /// <summary>
        /// Renders Excel file to stream.
        /// </summary>
        public void Render()
        {
            this._sheet.Workbook.RemoveSheetAt(0);
            this._sheet.Workbook.ToReponseStream(this._fileName);
        }

        #endregion Export to stream

        #region Cell Range methods

        /// <summary>
        /// Writes cell range value.
        /// </summary>
        /// <param name="rowIndex">Start row index.</param>
        /// <param name="totalRow">Number of rows that will be spanned.</param>
        /// <param name="colIndex">Start column index.</param>
        /// <param name="totalCol">Number of columns that will be spanned.</param>
        /// <param name="value">Value that will be filled into the cell range.</param>
        public void WriteCellRange(int rowIndex, int totalRow, int colIndex, int totalCol, object value)
        {
            var cellRange = new CellRangeAddress(rowIndex, rowIndex + totalRow - 1, colIndex, colIndex + totalCol - 1);
            this._sheet.AddMergedRegion(cellRange);

            var cell = this._sheet.GetCell(rowIndex, colIndex);
            cell.SetCellValue(value);
        }

        #endregion Cell Range methods

        #region Auto resize rows' height

        /// <summary>
        /// Auto resizes the rows' height.
        /// </summary>
        /// <param name="from">Start row index.</param>
        /// <param name="to">End row index.</param>
        public void AutosizeRows(int from, int to)
        {
            IRow currentRow = null;
            int totalLineBreaks = 0;

            from = Math.Max(from, 0);
            to = Math.Min(to, this._sheet.LastRowNum);

            for (int i = from; i <= to; i++)
            {
                currentRow = this._sheet.GetRow(i);
                totalLineBreaks = 0;

                foreach (ICell cell in currentRow.Cells)
                {
                    if (cell.CellType == CellType.String)
                    {
                        totalLineBreaks = Math.Max(totalLineBreaks, Count(cell.StringCellValue, BreakLine));
                    }
                }

                currentRow.HeightInPoints = totalLineBreaks * DefaultRowHeight;
            }
        }

        /// <summary>
        /// Auto resizes the rows' height.
        /// </summary>
        /// <param name="from">Start row index.</param>
        public void AutosizeRows(int from)
        {
            int to = this._sheet.LastRowNum;

            AutosizeRows(from, to);
        }

        #endregion Auto resize rows' height

        #region Auto resize columns' width

        /// <summary>
        /// Resizes columns' width automatically.
        /// </summary>
        /// <param name="from">Start column index.</param>
        /// <param name="to">End column index.</param>
        public void AutosizeColumns(int from, int to)
        {
            for (int i = from; i <= to; i++)
            {
                this._sheet.AutoSizeColumn(i, false);
            }
        }

        /// <summary>
        /// Resizes columns' width automatically.
        /// </summary>
        /// <param name="total">Number of columns that will be resized automatically.</param>
        public void AutosizeColumns(int total)
        {
            this.AutosizeColumns(0, total - 1);
        }

        #endregion Auto resize columns' width

        #region Synchronize columns' width

        /// <summary>
        /// Synchronizes columns' width of the templates with the current sheet.
        /// </summary>
        /// <param name="from">Start column index.</param>
        /// <param name="to">End column index.</param>
        public void SyncColumnsWidth(int from, int to)
        {
            for (int i = from; i <= to; i++)
            {
                this._sheet.SetColumnWidth(i, this._templateSheet.GetColumnWidth(i));
            }
        }

        /// <summary>
        /// Synchronizes columns' width of the templates with the current sheet.
        /// </summary>
        /// <param name="total">Number of columns that will be synchronized.</param>
        public void SyncColumnsWidth(int total)
        {
            this.SyncColumnsWidth(0, total - 1);
        }

        #endregion Synchronize columns' width

        #region Cell related methods

        /// <summary>
        /// Gets the cell style at a specific position.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>The cell style of cell at the passed position.</returns>
        public ICellStyle GetCellStyle(int rowIndex, int columnIndex)
        {
            ICell cell = this._templateSheet.GetCell(rowIndex, columnIndex);
            ICellStyle cellStyle = null;

            if (cell != null)
            {
                cellStyle = cell.CellStyle;
            }

            return cellStyle;
        }

        /// <summary>
        /// Gets the font at a specific position.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <param name="columnIndex">Index of the column.</param>
        /// <returns>The font of cell at the passed position.</returns>
        public IFont GetFont(int rowIndex, int columnIndex)
        {
            ICellStyle cellStyle = this.GetCellStyle(rowIndex, columnIndex);
            IFont font = null;

            if (cellStyle != null)
            {
                font = cellStyle.GetFont(this._templateSheet.Workbook);
            }

            return font;
        }

        #endregion Cell related methods

        #region Row related methods

        /// <summary>
        /// Writes values to a row of sheet.
        /// </summary>
        /// <param name="rowIndex">Index of original row that the style will be copied.</param>
        /// <param name="values">Value that will be filled into the row.</param>
        public void WriteRow(int rowIndex, object[] values)
        {
            IRow row = this.CreateRow(rowIndex);
            row.WriteCells(0, values);
        }

        /// <summary>
        /// Writes values to a number of rows of the sheet.
        /// </summary>
        /// <param name="rowIndex">Index of original row that the style will be copied.</param>
        /// <param name="arrayList">List of values which each of its items using to filled into a row.</param>
        public void WriteRows(int rowIndex, List<object[]> arrayList)
        {
            int total = arrayList.Count;
            IRow row = this.CreateRows(rowIndex, total);
            for (int i = 0; i < total; i++)
            {
                row.WriteCells(0, arrayList[i]);
                row = this._sheet.GetRow(row.RowNum + 1);
            }
        }

        /// <summary>
        /// Writes values to a row in the sheet.
        /// </summary>
        /// <param name="rowIndex">Index of original row that the style will be copied.</param>
        /// <param name="value">Value that will be filled into the row.</param>
        public void WriteRow(int rowIndex, object value)
        {
            List<object> list = new List<object>();
            foreach (var propertyInfo in value.GetType().GetProperties())
            {
                list.Add(propertyInfo.GetValue(value, null));
            }

            WriteRow(rowIndex, list.ToArray());
        }

        /// <summary>
        /// Writes number of rows to the sheet.
        /// </summary>
        /// <param name="rowId">Index of original row that the style will be copied.</param>
        /// <param name="recordList">List of values that will be filled into rows.</param>
        public void WriteRows(int rowId, IEnumerable<object> recordList)
        {
            foreach (var record in recordList)
            {
                this.WriteRow(rowId, record);
            }
        }

        /// <summary>
        /// Copies rows.
        /// </summary>
        /// <param name="rowIndex">Index of original row that the style will be copied.</param>
        /// <param name="total">Number of rows that will be created.</param>
        /// <returns>First instance in the list of created rows.</returns>
        public IRow CopyRows(int rowIndex, int total)
        {
            return CreateRows(rowIndex, total);
        }

        /// <summary>
        /// Creates a row.
        /// </summary>
        /// <param name="rowId">Index of original row that the style will be copied.</param>
        /// <returns>Row instance.</returns>
        private IRow CreateRow(int rowId)
        {
            return CreateRows(rowId, 1);
        }

        /// <summary>
        /// Creates number of rows.
        /// </summary>
        /// <param name="rowIndex">Index of original row that the style will be copied.</param>
        /// <param name="total">Number of rows that will be created.</param>
        /// <returns>First instance in the list of created rows.</returns>
        private IRow CreateRows(int rowIndex, int total)
        {
            this._sheet.CopyRows(this._templateSheet, rowIndex, this._rowId, total);
            this._rowId += total;
            return this._sheet.GetRow(this._rowId - total);
        }

        #endregion Row related methods

        #region String helpers

        /// <summary>
        /// Counts number of appearances of a text.
        /// </summary>
        /// <param name="str">The string that needs to count the text.</param>
        /// <param name="countText">The counting text.</param>
        /// <returns>Number of appearances of a text in the string.</returns>
        private int Count(string str, string countText)
        {
            return ((str.Length - str.Replace(countText, string.Empty).Length) / countText.Length) + 1;
        }

        #endregion String helpers
    }
}