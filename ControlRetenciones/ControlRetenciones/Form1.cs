using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using DocumentFormat.OpenXml.Presentation;
using NPOI.POIFS.FileSystem;
using Newtonsoft.Json;

namespace ControlRetenciones
{
    public partial class Form1 : Form
    {
        public List<int> columnas { get; set; }
        public int IndiceColumnaCuitAFIP { get; set; }
        public int IndiceColumnaDenominacionAFIP { get; set; }
        public int IndiceColumnaFechaAFIP { get; set; }
        public int IndiceColumnaCertificadoAFIP { get; set; }
        public int IndiceColumnaImporteAFIP { get; set; }

        List<XLColor> coloresCoincide = new List<XLColor>
        {
            XLColor.FromArgb(255, 204, 255, 204), // Tono de verde claro
        };

        List<XLColor> coloresNoCoincide = new List<XLColor>
        {
            XLColor.FromArgb(255, 255, 204, 204), // Tono de rojo claro
        };

        double totalAFIP = 0;
        double totalContabilidad = 0;
        double totalRojoAfip = 0;
        double totalRojoContabilidad = 0;

        public Form1()
        {
            InitializeComponent();

            columnas = new List<int>();

            IndiceColumnaCuitAFIP = 1;

            IndiceColumnaDenominacionAFIP = 2;

            IndiceColumnaFechaAFIP = 7;

            IndiceColumnaCertificadoAFIP = 8;

            IndiceColumnaImporteAFIP = 10;

            // Establecer el estilo del borde y deshabilitar el cambio de tamaño
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Establecer el tamaño mínimo y máximo para evitar el cambio de tamaño
            this.MinimumSize = this.MaximumSize = this.Size;

            // Ocultar el PictureBox al iniciar
            pictureBoxRuedaCargando.Visible = false;

            InicializarYMostrarEsquemas();
        }

        // Clase ExcelHelper definida fuera de la clase Form1
        public static class ExcelHelper
        {
            public static IWorkbook GetWorkbook(string filePath)
            {
                IWorkbook workbook = null;

                try
                {
                    using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        if (Path.GetExtension(filePath).ToLower() == ".xlsx")
                        {
                            workbook = new XSSFWorkbook(fs);
                        }
                        else if (Path.GetExtension(filePath).ToLower() == ".xls")
                        {
                            workbook = new HSSFWorkbook(fs);
                        }
                        else
                        {
                            throw new Exception("El archivo no tiene una extensión de Excel válida.");
                        }
                    }
                }
                catch (OfficeXmlFileException)
                {
                    // Muestra una notificación o cartel de información
                    MessageBox.Show("Hay un archivo en formato XLS. Abrir el Excel, guardarlo como nuevo y volver a ejecuar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Cierra el programa
                    Environment.Exit(1);
                }

                return workbook;
            }
        }
        private void btnSeleccionarArchivo1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos Excel|*.xlsx;*.xls";
                openFileDialog.Title = "Seleccionar primer archivo Excel";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Muestra la ruta seleccionada en el TextBox correspondiente
                    txtRutaArchivo1.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnSeleccionarArchivo2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos Excel|*.xlsx;*.xls";
                openFileDialog.Title = "Seleccionar segundo archivo Excel";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Muestra la ruta seleccionada en el TextBox correspondiente
                    txtRutaArchivo2.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnCarpeta_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog openFileDialog = new FolderBrowserDialog())
            {
                openFileDialog.Description = "Seleccionar una carpeta";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Muestra la ruta seleccionada en el TextBox correspondiente
                    textBoxReporte.Text = openFileDialog.SelectedPath;
                }
            }
        }

        private async void btnProcesar_Click(object sender, EventArgs e)
        {
            // Mostrar el PictureBox antes de comenzar el proceso
            pictureBoxRuedaCargando.Visible = true;

            string pathfileArchivo1 = txtRutaArchivo1.Text;
            string pathfileArchivo2 = txtRutaArchivo2.Text;
            string pathfileReporte = textBoxReporte.Text;

            // Convertir archivos XLS a XLSX si es necesario
            if (Path.GetExtension(pathfileArchivo1).ToLower() == ".xls")
            {
                pathfileArchivo1 = ConvertXlsToXlsx(pathfileArchivo1);
            }

            if (Path.GetExtension(pathfileArchivo2).ToLower() == ".xls")
            {
                pathfileArchivo2 = ConvertXlsToXlsx(pathfileArchivo2);
            }

            // Define una lista de esquemas
            List<Esquema> listaEsquemas = new List<Esquema>();

            // Ruta del archivo Esquemas en el directorio de la aplicación
            string filePath = Path.Combine(Application.StartupPath, "Esquemas.txt");

            // Cargar los esquemas desde el archivo
            CargarEsquemasDesdeArchivo(filePath, listaEsquemas);

            if (columnas != null)
            {
                this.columnas.Clear();
            }

            if (comboBoxEsquemas.SelectedItem != null)
            {
                foreach (Esquema esquema in listaEsquemas)
                {
                    if (comboBoxEsquemas.SelectedItem.ToString() == esquema.Nombre)
                    {
                        this.columnas.Add(esquema.IndiceCuit);
                        this.columnas.Add(esquema.IndiceFecha);
                        this.columnas.Add(esquema.IndiceCertificado);
                        this.columnas.Add(esquema.IndiceImporte);
                    }
                }
            }

            // Realizar el proceso de manera asíncrona
            await Task.Run(() => CompararArchivos(pathfileArchivo1, pathfileArchivo2));

            CrearReporteExcel(pathfileReporte);

            // Ocultar el PictureBox al finalizar el proceso
            pictureBoxRuedaCargando.Visible = false;

            // Muestra un mensaje de éxito
            MessageBox.Show("Proceso completado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Muestra un mensaje de éxito
            MessageBox.Show($"Se creo el reporte en {pathfileReporte}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //codigo para convertir en XLSX los archivos XLS (el que se baja desde AFIP)
        private string ConvertXlsToXlsx(string xlsFilePath)
        {
            // Ruta para guardar el archivo XLSX
            string xlsxFilePath = Path.ChangeExtension(xlsFilePath, ".xlsx");

            // Crear un nuevo libro de trabajo XLSX
            using (var workbookXlsx = new XSSFWorkbook())
            {
                // Obtener el libro de trabajo XLS
                using (var fs = new FileStream(xlsFilePath, FileMode.Open, FileAccess.Read))
                {
                    var workbookXls = ExcelHelper.GetWorkbook(xlsFilePath);

                    // Copiar todas las hojas de trabajo de XLS a XLSX
                    for (int i = 0; i < workbookXls.NumberOfSheets; i++)
                    {
                        var sheetXls = workbookXls.GetSheetAt(i);
                        var sheetXlsx = workbookXlsx.CreateSheet(sheetXls.SheetName);

                        for (int row = 0; row <= sheetXls.LastRowNum; row++)
                        {
                            var rowXls = sheetXls.GetRow(row);
                            var rowXlsx = sheetXlsx.CreateRow(row);

                            if (rowXls != null)
                            {
                                for (int col = 0; col <= rowXls.LastCellNum; col++)
                                {
                                    var cellXls = rowXls.GetCell(col);
                                    var cellXlsx = rowXlsx.CreateCell(col);

                                    if (cellXls != null)
                                    {
                                        switch (cellXls.CellType)
                                        {
                                            case NPOI.SS.UserModel.CellType.Numeric:
                                                cellXlsx.SetCellValue(cellXls.NumericCellValue);
                                                break;
                                            case NPOI.SS.UserModel.CellType.String:
                                                cellXlsx.SetCellValue(cellXls.StringCellValue);
                                                break;
                                                // Agregar otros casos según sea necesario
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Guardar el archivo XLSX
                using (var fs = new FileStream(xlsxFilePath, FileMode.Create, FileAccess.Write))
                {
                    workbookXlsx.Write(fs);
                }
            }

            return xlsxFilePath;
        }

        private void CompararArchivos(string pathfileArchivo1, string pathfileArchivo2)
        {
            int indiceColumnaCUIT = columnas[0];
            int indiceColumnaFecha = columnas[1];
            int indiceColumnaCertificado = columnas[2];
            int indiceColumnaImporte = columnas[3];

            // LLamada para comparar por cuit, importe y fecha (mayor exactitud)
            CompararArchivosPorCuitFechaImporte(pathfileArchivo1, pathfileArchivo2, 0.1, indiceColumnaCUIT, indiceColumnaFecha, indiceColumnaImporte);

            // Segunda llamada para que compare de manera exacta sin tener en cuenta la fecha
            CompararArchivosPorCuit(pathfileArchivo1, pathfileArchivo2, 0.1, indiceColumnaCUIT, indiceColumnaImporte);

            // Segunda llamada para que compare con una tolerancia de 1 en el importe sin tener en cuenta la fecha
            CompararArchivosPorCuit(pathfileArchivo1, pathfileArchivo2, 1, indiceColumnaCUIT, indiceColumnaImporte);

            // Tercera y ultima llamada para que compare con una tolerancia de 2 en el importe sin tener en cuenta la fecha
            CompararArchivosPorCuit(pathfileArchivo1, pathfileArchivo2, 2, indiceColumnaCUIT, indiceColumnaImporte);

            if (indiceColumnaCertificado != 1)
            {
                // Cuarta comparacion para comparar por certificado e importe 
                CompararArchivosPorCertificado(pathfileArchivo1, pathfileArchivo2, indiceColumnaCertificado, indiceColumnaImporte);
            }

            // Quinta comparacion para comparar por fecha e importe exactos sin tener en cuenta CUIT        
            CompararArchivosPorFechaEImporte(pathfileArchivo1, pathfileArchivo2, indiceColumnaImporte, indiceColumnaFecha);

            //Marcar en rojo los que no coinciden
            MarcarNoCoincidentesEnRojo(pathfileArchivo1, 1, indiceColumnaImporte);
            MarcarNoCoincidentesEnRojo(pathfileArchivo2, 2, IndiceColumnaImporteAFIP);
        }

        private void CompararArchivosPorCuitFechaImporte(string pathfileArchivo1, string pathfileArchivo2, double tolerancia, int indiceColumnaCuitContabilidad, int indiceColumnaFechaContabilidad, int indiceColumnaImporteContabilidad)
        {
            using (var workbookArchivo1 = new XLWorkbook(pathfileArchivo1))
            {
                using (var workbookArchivo2 = new XLWorkbook(pathfileArchivo2))
                {
                    var worksheetArchivo1 = workbookArchivo1.Worksheets.First();
                    var worksheetArchivo2 = workbookArchivo2.Worksheets.First();

                    int ultimaColumnaContabilidad = worksheetArchivo1.LastColumnUsed().ColumnNumber();
                    int ultimaColumnaAFIP = worksheetArchivo2.LastColumnUsed().ColumnNumber();

                    worksheetArchivo1.Cell(1, ultimaColumnaContabilidad + 1).Value = "Comparado con fila";
                    worksheetArchivo2.Cell(1, ultimaColumnaAFIP + 1).Value = "Comparado con fila";

                    int indiceColor = 0;

                    // Diccionarios para almacenar filas por Nro. Doc. / CUIT 
                    Dictionary<string, List<(int, bool, DateTime)>> diccionarioArchivo1 = new Dictionary<string, List<(int, bool, DateTime)>>();
                    Dictionary<string, List<(int, bool, DateTime)>> diccionarioArchivo2 = new Dictionary<string, List<(int, bool, DateTime)>>();

                    // Llenar diccionario para el archivo 1
                    for (int filaArchivo1 = 2; filaArchivo1 <= worksheetArchivo1.RowsUsed().Count(); filaArchivo1++)
                    {
                        string valorCeldaNroDoc = worksheetArchivo1.Cell(filaArchivo1, indiceColumnaCuitContabilidad).GetString();
                        string nroDoc = valorCeldaNroDoc;
                        nroDoc = nroDoc.Replace("-", "");

                        string stringFechaArchivo1 = worksheetArchivo1.Cell(filaArchivo1, indiceColumnaFechaContabilidad).GetString();
                        DateTime fechaArchivo1 = new DateTime();
                        if (stringFechaArchivo1.Contains("/"))
                        {
                            fechaArchivo1 = DateTime.Parse(stringFechaArchivo1);
                        }
                        else
                        {
                            fechaArchivo1 = worksheetArchivo1.Cell(filaArchivo1, indiceColumnaFechaContabilidad).GetDateTime();
                        }

                        XLColor colorArchivo1 = worksheetArchivo1.Cell(filaArchivo1, indiceColumnaImporteContabilidad).Style.Fill.BackgroundColor;
                        if (colorArchivo1 == XLColor.FromArgb(255, 204, 255, 204)) // Verde claro
                        {
                            continue; // Saltar filas ya marcadas en verde
                        }

                        if (diccionarioArchivo1.ContainsKey(nroDoc))
                        {
                            diccionarioArchivo1[nroDoc].Add((filaArchivo1, false, fechaArchivo1));
                        }
                        else
                        {
                            diccionarioArchivo1[nroDoc] = new List<(int, bool, DateTime)> { (filaArchivo1, false, fechaArchivo1) };
                        }
                    }

                    // Llenar diccionario para el archivo 2
                    for (int filaArchivo2 = 2; filaArchivo2 <= worksheetArchivo2.RowsUsed().Count(); filaArchivo2++)
                    {
                        string valorCeldaCuitAgente = worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaCuitAFIP).GetString();
                        string cuitAgente = valorCeldaCuitAgente;
                        cuitAgente = cuitAgente.Replace("-", "");

                        string stringFechaArchivo2 = worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaFechaAFIP).GetString();
                        DateTime fechaArchivo2 = new DateTime();
                        if (stringFechaArchivo2.Contains("/"))
                        {
                            fechaArchivo2 = DateTime.Parse(stringFechaArchivo2);
                        }
                        else
                        {
                            fechaArchivo2 = worksheetArchivo1.Cell(filaArchivo2, indiceColumnaFechaContabilidad).GetDateTime();
                        }

                        XLColor colorArchivo2 = worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaImporteAFIP).Style.Fill.BackgroundColor;
                        if (colorArchivo2 == XLColor.FromArgb(255, 204, 255, 204)) // Verde claro
                        {
                            continue; // Saltar filas ya marcadas en verde
                        }

                        if (diccionarioArchivo2.ContainsKey(cuitAgente))
                        {
                            diccionarioArchivo2[cuitAgente].Add((filaArchivo2, false, fechaArchivo2));
                        }
                        else
                        {
                            diccionarioArchivo2[cuitAgente] = new List<(int, bool, DateTime)> { (filaArchivo2, false, fechaArchivo2) };
                        }
                    }

                    // Bucle principal para comparar y marcar en verde
                    foreach (var claveArchivo1 in diccionarioArchivo1.Keys.ToList())
                    {
                        if (diccionarioArchivo2.TryGetValue(claveArchivo1, out List<(int, bool, DateTime)> filasArchivo2))
                        {
                            foreach ((int filaArchivo1, bool comparado, DateTime fechaArchivo1) in diccionarioArchivo1[claveArchivo1].ToList())
                            {
                                if (comparado == false) // Solo si aún no se ha comparado esta fila
                                {
                                    double importeArchivo1 = worksheetArchivo1.Cell(filaArchivo1, indiceColumnaImporteContabilidad).GetValue<double>();
                                    int ban = 0;

                                    foreach ((int filaArchivo2, bool comparadoArchivo2, DateTime fechaArchivo2) in filasArchivo2.ToList())
                                    {
                                        if (comparadoArchivo2 == false) //Solo si aún no se ha comparado esta fila
                                        {
                                            string valorCeldaImporterArchivo2 = worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaImporteAFIP).GetString();
                                            string valorCeldaImporterArchivo2SinComa = valorCeldaImporterArchivo2.Replace(",", ".");
                                            double importeArchivo2 = double.Parse(valorCeldaImporterArchivo2SinComa, CultureInfo.InvariantCulture);
                                            double resultado = Math.Abs(importeArchivo1 - importeArchivo2);

                                            // Comparar con una tolerancia recibida por parametro
                                            if (Math.Abs(importeArchivo1 - importeArchivo2) <= tolerancia && fechaArchivo1 == fechaArchivo2)
                                            {
                                                // Obtén el siguiente color de la lista
                                                XLColor color = coloresCoincide[indiceColor % coloresCoincide.Count];

                                                worksheetArchivo1.Cell(filaArchivo1, indiceColumnaImporteContabilidad).Style.Fill.BackgroundColor = color;
                                                worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaImporteAFIP).Style.Fill.BackgroundColor = color;

                                                // Marcar como comparado
                                                var indiceArchivo1 = diccionarioArchivo1[claveArchivo1].FindIndex(f => f.Item1 == filaArchivo1);
                                                var indiceArchivo2 = diccionarioArchivo2[claveArchivo1].FindIndex(f => f.Item1 == filaArchivo2);

                                                diccionarioArchivo1[claveArchivo1][indiceArchivo1] = (filaArchivo1, true, fechaArchivo1);
                                                diccionarioArchivo2[claveArchivo1][indiceArchivo2] = (filaArchivo2, true, fechaArchivo2);

                                                worksheetArchivo1.Cell(filaArchivo1, ultimaColumnaContabilidad + 1).Value = filaArchivo2;
                                                worksheetArchivo2.Cell(filaArchivo2, ultimaColumnaAFIP + 1).Value = filaArchivo1;

                                                indiceColor++; // Incrementar el índice de color

                                                ban = 1;

                                                break; // Salir del bucle interno después de encontrar una coincidencia
                                            }
                                        }
                                    }
                                    if (ban == 0)
                                    {
                                        // Obtén el siguiente color de la lista
                                        XLColor color = coloresNoCoincide[indiceColor % coloresCoincide.Count];
                                        worksheetArchivo1.Cell(filaArchivo1, indiceColumnaImporteContabilidad).Style.Fill.BackgroundColor = color;

                                        importeArchivo1 = worksheetArchivo1.Cell(filaArchivo1, indiceColumnaImporteContabilidad).GetValue<double>();
                                    }
                                }
                            }
                        }
                    }


                    workbookArchivo1.SaveAs(pathfileArchivo1);
                    workbookArchivo2.SaveAs(pathfileArchivo2);
                }
            }

        }

        private void CompararArchivosPorCuit(string pathfileArchivo1, string pathfileArchivo2, double tolerancia, int indiceColumnaCuitContabilidad, int indiceColumnaImporteContabilidad)
        {
            using (var workbookArchivo1 = new XLWorkbook(pathfileArchivo1))
            {
                using (var workbookArchivo2 = new XLWorkbook(pathfileArchivo2))
                {
                    var worksheetArchivo1 = workbookArchivo1.Worksheets.First();
                    var worksheetArchivo2 = workbookArchivo2.Worksheets.First();

                    int ultimaColumnaContabilidad = worksheetArchivo1.LastColumnUsed().ColumnNumber();
                    int ultimaColumnaAFIP = worksheetArchivo2.LastColumnUsed().ColumnNumber();

                    int indiceColor = 0;

                    // Diccionarios para almacenar filas por Nro. Doc. / CUIT
                    Dictionary<string, List<(int, bool)>> diccionarioArchivo1 = new Dictionary<string, List<(int, bool)>>();
                    Dictionary<string, List<(int, bool)>> diccionarioArchivo2 = new Dictionary<string, List<(int, bool)>>();

                    // Llenar diccionario para el archivo 1
                    for (int filaArchivo1 = 2; filaArchivo1 <= worksheetArchivo1.RowsUsed().Count(); filaArchivo1++)
                    {
                        string valorCeldaNroDoc = worksheetArchivo1.Cell(filaArchivo1, indiceColumnaCuitContabilidad).GetString();
                        string nroDoc = valorCeldaNroDoc;
                        nroDoc = nroDoc.Replace("-", "");

                        XLColor colorArchivo1 = worksheetArchivo1.Cell(filaArchivo1, indiceColumnaImporteContabilidad).Style.Fill.BackgroundColor;
                        if (colorArchivo1 == XLColor.FromArgb(255, 204, 255, 204)) // Verde claro
                        {
                            continue; // Saltar filas ya marcadas en verde
                        }

                        if (diccionarioArchivo1.ContainsKey(nroDoc))
                        {
                            diccionarioArchivo1[nroDoc].Add((filaArchivo1, false));
                        }
                        else
                        {
                            diccionarioArchivo1[nroDoc] = new List<(int, bool)> { (filaArchivo1, false) };
                        }
                    }

                    // Llenar diccionario para el archivo 2
                    for (int filaArchivo2 = 2; filaArchivo2 <= worksheetArchivo2.RowsUsed().Count(); filaArchivo2++)
                    {
                        string valorCeldaCuitAgente = worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaCuitAFIP).GetString();
                        string cuitAgente = valorCeldaCuitAgente;
                        cuitAgente = cuitAgente.Replace("-", "");

                        XLColor colorArchivo2 = worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaImporteAFIP).Style.Fill.BackgroundColor;
                        if (colorArchivo2 == XLColor.FromArgb(255, 204, 255, 204)) // Verde claro
                        {
                            continue; // Saltar filas ya marcadas en verde
                        }

                        if (diccionarioArchivo2.ContainsKey(cuitAgente))
                        {
                            diccionarioArchivo2[cuitAgente].Add((filaArchivo2, false));
                        }
                        else
                        {
                            diccionarioArchivo2[cuitAgente] = new List<(int, bool)> { (filaArchivo2, false) };
                        }
                    }


                    // Bucle principal para comparar y marcar en verde
                    foreach (var claveArchivo1 in diccionarioArchivo1.Keys.ToList())
                    {
                        if (diccionarioArchivo2.TryGetValue(claveArchivo1, out List<(int, bool)> filasArchivo2))
                        {
                            foreach ((int filaArchivo1, bool comparado) in diccionarioArchivo1[claveArchivo1].ToList())
                            {
                                if (comparado == false) // Solo si aún no se ha comparado esta fila
                                {
                                    double importeArchivo1 = worksheetArchivo1.Cell(filaArchivo1, indiceColumnaImporteContabilidad).GetValue<double>();

                                    int ban = 0;

                                    foreach ((int filaArchivo2, bool comparadoArchivo2) in filasArchivo2.ToList())
                                    {
                                        if (comparadoArchivo2 == false) //Solo si aún no se ha comparado esta fila
                                        {
                                            string valorCeldaImporterArchivo2 = worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaImporteAFIP).GetString();
                                            string valorCeldaImporterArchivo2SinComa = valorCeldaImporterArchivo2.Replace(",", ".");
                                            double importeArchivo2 = double.Parse(valorCeldaImporterArchivo2SinComa, CultureInfo.InvariantCulture);
                                            double resultado = Math.Abs(importeArchivo1 - importeArchivo2);

                                            // Comparar con una tolerancia recibida por parametro
                                            if (Math.Abs(importeArchivo1 - importeArchivo2) <= tolerancia)
                                            {
                                                // Obtén el siguiente color de la lista
                                                XLColor color = coloresCoincide[indiceColor % coloresCoincide.Count];

                                                worksheetArchivo1.Cell(filaArchivo1, indiceColumnaImporteContabilidad).Style.Fill.BackgroundColor = color;
                                                worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaImporteAFIP).Style.Fill.BackgroundColor = color;

                                                // Marcar como comparado
                                                var indiceArchivo1 = diccionarioArchivo1[claveArchivo1].FindIndex(f => f.Item1 == filaArchivo1);
                                                var indiceArchivo2 = diccionarioArchivo2[claveArchivo1].FindIndex(f => f.Item1 == filaArchivo2);

                                                diccionarioArchivo1[claveArchivo1][indiceArchivo1] = (filaArchivo1, true);
                                                diccionarioArchivo2[claveArchivo1][indiceArchivo2] = (filaArchivo2, true);

                                                worksheetArchivo1.Cell(filaArchivo1, ultimaColumnaContabilidad).Value = filaArchivo2;
                                                worksheetArchivo2.Cell(filaArchivo2, ultimaColumnaAFIP).Value = filaArchivo1;

                                                indiceColor++; // Incrementar el índice de color

                                                ban = 1;

                                                break; // Salir del bucle interno después de encontrar una coincidencia
                                            }
                                        }
                                    }
                                    if (ban == 0)
                                    {
                                        // Obtén el siguiente color de la lista
                                        XLColor color = coloresNoCoincide[indiceColor % coloresCoincide.Count];
                                        worksheetArchivo1.Cell(filaArchivo1, indiceColumnaImporteContabilidad).Style.Fill.BackgroundColor = color;

                                        importeArchivo1 = worksheetArchivo1.Cell(filaArchivo1, indiceColumnaImporteContabilidad).GetValue<double>();
                                    }
                                }
                            }
                        }
                    }


                    workbookArchivo1.SaveAs(pathfileArchivo1);
                    workbookArchivo2.SaveAs(pathfileArchivo2);
                }
            }
        }

        private void CompararArchivosPorFechaEImporte(string pathfileArchivo1, string pathfileArchivo2, int IndiceColumnaImporteContabilidad, int IndiceColumnaFechaContabilidad)
        {
            using (var workbookArchivo1 = new XLWorkbook(pathfileArchivo1))
            {
                using (var workbookArchivo2 = new XLWorkbook(pathfileArchivo2))
                {
                    var worksheetArchivo1 = workbookArchivo1.Worksheets.First();
                    var worksheetArchivo2 = workbookArchivo2.Worksheets.First();

                    int ultimaColumnaContabilidad = worksheetArchivo1.LastColumnUsed().ColumnNumber();
                    int ultimaColumnaAFIP = worksheetArchivo2.LastColumnUsed().ColumnNumber();

                    int indiceColor = 0;

                    // Diccionarios para almacenar filas marcadas en rojo por fecha
                    Dictionary<DateTime, List<int>> diccionarioFechaArchivo1 = new Dictionary<DateTime, List<int>>();
                    Dictionary<DateTime, List<int>> diccionarioFechaArchivo2 = new Dictionary<DateTime, List<int>>();

                    int contador = worksheetArchivo1.RowsUsed().Count();
                    int contador2 = worksheetArchivo2.RowsUsed().Count();

                    // Llenar diccionario para el archivo 1
                    for (int filaArchivo1 = 2; filaArchivo1 <= worksheetArchivo1.RowsUsed().Count(); filaArchivo1++)
                    {
                        XLColor colorArchivo1 = worksheetArchivo1.Cell(filaArchivo1, IndiceColumnaImporteContabilidad).Style.Fill.BackgroundColor;

                        if (colorArchivo1 == XLColor.FromArgb(255, 204, 255, 204)) // Verde claro
                        {
                            continue; // Saltar filas ya marcadas en verde
                        }

                        string stringFechaArchivo1 = worksheetArchivo1.Cell(filaArchivo1, IndiceColumnaFechaContabilidad).GetString();
                        DateTime fechaArchivo1 = new DateTime();
                        if (stringFechaArchivo1.Contains("/"))
                        {
                            fechaArchivo1 = DateTime.Parse(stringFechaArchivo1);
                        }
                        else
                        {
                            fechaArchivo1 = worksheetArchivo1.Cell(filaArchivo1, IndiceColumnaFechaContabilidad).GetDateTime();
                        }

                        if (diccionarioFechaArchivo1.ContainsKey(fechaArchivo1))
                        {
                            diccionarioFechaArchivo1[fechaArchivo1].Add(filaArchivo1);
                        }
                        else
                        {
                            diccionarioFechaArchivo1[fechaArchivo1] = new List<int> { filaArchivo1 };
                        }
                    }

                    // Llenar diccionario para el archivo 2
                    for (int filaArchivo2 = 2; filaArchivo2 <= worksheetArchivo2.RowsUsed().Count(); filaArchivo2++)
                    {
                        XLColor colorArchivo2 = worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaImporteAFIP).Style.Fill.BackgroundColor;

                        if (colorArchivo2 == XLColor.FromArgb(255, 204, 255, 204)) // Verde claro
                        {
                            continue; // Saltar filas ya marcadas en verde
                        }

                        string stringFechaArchivo2 = worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaFechaAFIP).GetString();
                        DateTime fechaArchivo2 = new DateTime();
                        if (stringFechaArchivo2.Contains("/"))
                        {
                            fechaArchivo2 = DateTime.Parse(stringFechaArchivo2);
                        }
                        else
                        {
                            fechaArchivo2 = worksheetArchivo1.Cell(filaArchivo2, IndiceColumnaFechaAFIP).GetDateTime();
                        }

                        if (diccionarioFechaArchivo2.ContainsKey(fechaArchivo2))
                        {
                            diccionarioFechaArchivo2[fechaArchivo2].Add(filaArchivo2);
                        }
                        else
                        {
                            diccionarioFechaArchivo2[fechaArchivo2] = new List<int> { filaArchivo2 };
                        }
                    }

                    // Bucle adicional para comparar por fecha e importe y marcar en verde si coinciden
                    foreach (var fechaArchivo1 in diccionarioFechaArchivo1.Keys)
                    {
                        foreach (int filaArchivo1 in diccionarioFechaArchivo1[fechaArchivo1])
                        {
                            double importeArchivo1 = worksheetArchivo1.Cell(filaArchivo1, IndiceColumnaImporteContabilidad).GetValue<double>();

                            if (diccionarioFechaArchivo2.TryGetValue(fechaArchivo1, out List<int> filasFechaArchivo2))
                            {
                                foreach (int filaArchivo2 in filasFechaArchivo2.ToList())
                                {

                                    double importeArchivo2 = worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaImporteAFIP).GetValue<double>();

                                    // Comparar por importe
                                    if (Math.Abs(importeArchivo1 - importeArchivo2) <= 10)
                                    {
                                        // Obtén el siguiente color de la lista para marcar en verde
                                        XLColor colorVerde = coloresCoincide[indiceColor % coloresCoincide.Count];

                                        worksheetArchivo1.Cell(filaArchivo1, IndiceColumnaImporteContabilidad).Style.Fill.BackgroundColor = colorVerde;
                                        worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaImporteAFIP).Style.Fill.BackgroundColor = colorVerde;

                                        worksheetArchivo1.Cell(filaArchivo1, ultimaColumnaContabilidad).Value = filaArchivo2;
                                        worksheetArchivo2.Cell(filaArchivo2, ultimaColumnaAFIP).Value = filaArchivo1;

                                        indiceColor++; // Incrementar el índice de color

                                        break; // Salir del bucle interno después de encontrar una coincidencia
                                    }
                                }
                            }
                        }
                    }

                    workbookArchivo1.SaveAs(pathfileArchivo1);
                    workbookArchivo2.SaveAs(pathfileArchivo2);
                }
            }
        }

        private void CompararArchivosPorCertificado(string pathfileArchivo1, string pathfileArchivo2, int IndiceColumnaCertificadoContabilidad, int IndiceColumnaImporteContabilidad)
        {
            if (IndiceColumnaCertificadoContabilidad != -1)
            {
                using (var workbookArchivo1 = new XLWorkbook(pathfileArchivo1))
                {
                    using (var workbookArchivo2 = new XLWorkbook(pathfileArchivo2))
                    {
                        var worksheetArchivo1 = workbookArchivo1.Worksheets.First();
                        var worksheetArchivo2 = workbookArchivo2.Worksheets.First();

                        int ultimaColumnaContabilidad = worksheetArchivo1.LastColumnUsed().ColumnNumber();
                        int ultimaColumnaAFIP = worksheetArchivo2.LastColumnUsed().ColumnNumber();

                        int indiceColor = 0;

                        // Nuevo diccionario para almacenar filas no marcadas en verde por número de certificado
                        Dictionary<string, List<int>> diccionarioCertificadoNoMarcadoArchivo1 = new Dictionary<string, List<int>>();
                        Dictionary<string, List<int>> diccionarioCertificadoNoMarcadoArchivo2 = new Dictionary<string, List<int>>();

                        // Llenar diccionario para el archivo 1
                        for (int filaArchivo1 = 2; filaArchivo1 <= worksheetArchivo1.RowsUsed().Count(); filaArchivo1++)
                        {
                            string certificadoArchivo1 = worksheetArchivo1.Cell(filaArchivo1, IndiceColumnaCertificadoContabilidad).GetString();
                            XLColor colorArchivo1 = worksheetArchivo1.Cell(filaArchivo1, IndiceColumnaImporteContabilidad).Style.Fill.BackgroundColor;

                            if (colorArchivo1 == XLColor.FromArgb(255, 204, 255, 204)) // Verde claro
                            {
                                continue; // Saltar filas ya marcadas en verde
                            }

                            // Manipulación del número de certificado para que coincida con el formato del archivo 2
                            string certificadoArchivo1Formateado = certificadoArchivo1.Replace("-", "").Substring(4);

                            if (diccionarioCertificadoNoMarcadoArchivo1.ContainsKey(certificadoArchivo1Formateado))
                            {
                                diccionarioCertificadoNoMarcadoArchivo1[certificadoArchivo1Formateado].Add(filaArchivo1);
                            }
                            else
                            {
                                diccionarioCertificadoNoMarcadoArchivo1[certificadoArchivo1Formateado] = new List<int> { filaArchivo1 };
                            }
                        }

                        // Llenar diccionario para el archivo 2
                        for (int filaArchivo2 = 2; filaArchivo2 <= worksheetArchivo2.RowsUsed().Count(); filaArchivo2++)
                        {
                            string certificadoArchivo2 = worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaCertificadoAFIP).GetString();
                            XLColor colorArchivo2 = worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaImporteAFIP).Style.Fill.BackgroundColor;

                            if (colorArchivo2 == XLColor.FromArgb(255, 204, 255, 204)) // Verde claro
                            {
                                continue; // Saltar filas ya marcadas en verde
                            }

                            if (diccionarioCertificadoNoMarcadoArchivo2.ContainsKey(certificadoArchivo2))
                            {
                                diccionarioCertificadoNoMarcadoArchivo2[certificadoArchivo2].Add(filaArchivo2);
                            }
                            else
                            {
                                diccionarioCertificadoNoMarcadoArchivo2[certificadoArchivo2] = new List<int> { filaArchivo2 };
                            }
                        }

                        // Bucle adicional para comparar por número de certificado
                        foreach (var certificadoArchivo1 in diccionarioCertificadoNoMarcadoArchivo1.Keys.ToList())
                        {
                            foreach (int filaArchivo1 in diccionarioCertificadoNoMarcadoArchivo1[certificadoArchivo1].ToList())
                            {
                                double importeArchivo1 = worksheetArchivo1.Cell(filaArchivo1, IndiceColumnaImporteContabilidad).GetValue<double>();

                                int ban = 0;

                                if (diccionarioCertificadoNoMarcadoArchivo2.TryGetValue(certificadoArchivo1, out List<int> filasCertificadoArchivo2))
                                {
                                    foreach (int filaArchivo2 in filasCertificadoArchivo2.ToList())
                                    {
                                        string valorCeldaImporterArchivo2 = worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaImporteAFIP).GetString();
                                        string valorCeldaImporterArchivo2SinComa = valorCeldaImporterArchivo2.Replace(",", ".");
                                        double importeArchivo2 = double.Parse(valorCeldaImporterArchivo2SinComa, CultureInfo.InvariantCulture);

                                        // Comparar con una tolerancia de ±10
                                        if (Math.Abs(importeArchivo1 - importeArchivo2) <= 10)
                                        {
                                            // Obtén el siguiente color de la lista
                                            XLColor color = coloresCoincide[indiceColor % coloresCoincide.Count];

                                            worksheetArchivo1.Cell(filaArchivo1, IndiceColumnaImporteContabilidad).Style.Fill.BackgroundColor = color;
                                            worksheetArchivo2.Cell(filaArchivo2, IndiceColumnaImporteAFIP).Style.Fill.BackgroundColor = color;

                                            // Marcar como comparado
                                            diccionarioCertificadoNoMarcadoArchivo1[certificadoArchivo1].Remove(filaArchivo1);
                                            diccionarioCertificadoNoMarcadoArchivo2[certificadoArchivo1].Remove(filaArchivo2);

                                            worksheetArchivo1.Cell(filaArchivo1, ultimaColumnaContabilidad).Value = filaArchivo2;
                                            worksheetArchivo2.Cell(filaArchivo2, ultimaColumnaAFIP).Value = filaArchivo1;

                                            indiceColor++; // Incrementar el índice de color

                                            ban = 1;
                                            break; // Salir del bucle interno después de encontrar una coincidencia
                                        }
                                    }
                                }

                                if (ban == 0)
                                {
                                    // Obtén el siguiente color de la lista
                                    XLColor color = coloresNoCoincide[indiceColor % coloresNoCoincide.Count];

                                    worksheetArchivo1.Cell(filaArchivo1, IndiceColumnaImporteContabilidad).Style.Fill.BackgroundColor = color;
                                }
                            }
                        }

                        // Guardar el archivo después de la comparación
                        workbookArchivo1.SaveAs(pathfileArchivo1);
                        workbookArchivo2.SaveAs(pathfileArchivo2);
                    }
                }
            }
        }

        private void MarcarNoCoincidentesEnRojo(string pathArchivo, int archivo, int IndiceColumnaImporte)
        {
            using (var workbookArchivo = new XLWorkbook(pathArchivo))
            {
                var worksheetArchivo = workbookArchivo.Worksheets.First();
                int indiceColor = 0;

                for (int fila = 2; fila <= worksheetArchivo.RowsUsed().Count(); fila++)
                {
                    var colorArchivo = worksheetArchivo.Cell(fila, IndiceColumnaImporte).Style.Fill.BackgroundColor;

                    if (colorArchivo != XLColor.FromArgb(255, 204, 255, 204) && colorArchivo != XLColor.FromArgb(255, 255, 204, 204))
                    {

                        // Obtén el siguiente color de la lista para marcar en rojo
                        XLColor colorRojo = coloresNoCoincide[indiceColor % coloresCoincide.Count];

                        // Marcar en rojo en el archivo
                        worksheetArchivo.Cell(fila, IndiceColumnaImporte).Style.Fill.BackgroundColor = colorRojo;

                        indiceColor++; // Incrementar el índice de color
                    }

                    string valorCeldaImporterArchivo = worksheetArchivo.Cell(fila, IndiceColumnaImporte).GetString();
                    string valorCeldaImporterArchivoConComa = valorCeldaImporterArchivo.Replace(",", ".");
                    double importeArchivo = double.Parse(valorCeldaImporterArchivoConComa, CultureInfo.InvariantCulture);

                    if (archivo == 1)
                    {
                        if (colorArchivo == XLColor.FromArgb(255, 204, 255, 204))
                        {
                            totalContabilidad += importeArchivo;
                        }
                        else
                        {
                            totalContabilidad += importeArchivo;
                            totalRojoContabilidad += importeArchivo;
                        }
                    }
                    else
                    {
                        if (colorArchivo == XLColor.FromArgb(255, 204, 255, 204))
                        {
                            totalAFIP += importeArchivo;
                        }
                        else
                        {
                            totalAFIP += importeArchivo;
                            totalRojoAfip += importeArchivo;
                        }
                    }
                }

                // Guardar el archivo
                workbookArchivo.SaveAs(pathArchivo);
            }
        }

        private void CrearReporteExcel(string rutaArchivo)
        {
            // Verificar si la ruta del archivo es válida
            if (string.IsNullOrWhiteSpace(rutaArchivo))
            {
                MessageBox.Show("La ruta del archivo no puede estar vacía", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Agregar la extensión .xlsx si no se proporciona
            if (Path.GetExtension(rutaArchivo) != ".xlsx")
            {
                rutaArchivo += "/REPORTE";
                rutaArchivo += ".xlsx";
            }

            // Crear un nuevo archivo de Excel
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte");

                // Configurar títulos y estilos
                var tituloStyle = worksheet.Style;
                tituloStyle.Font.Bold = true;

                // Retenciones AFIP
                worksheet.Cell("A1").Value = "Retenciones AFIP";
                worksheet.Cell("B1").Value = totalAFIP;
                worksheet.Cell("A2").Value = "Retenciones que no están en AFIP pero sí registradas";
                worksheet.Cell("B2").Value = totalRojoContabilidad;
                worksheet.Cell("A3").Value = "Total";
                worksheet.Cell("B3").FormulaA1 = "B1 + B2";

                // Aplicar estilo al fondo de las celdas de total y diferencia
                var fondoTotalDiferenciaStyle = worksheet.Cell("B1").Style;
                fondoTotalDiferenciaStyle.Fill.BackgroundColor = XLColor.FromHtml("#B93D7B");
                fondoTotalDiferenciaStyle = worksheet.Cell("B2").Style;
                fondoTotalDiferenciaStyle.Fill.BackgroundColor = XLColor.FromHtml("#B93D7B");
                fondoTotalDiferenciaStyle = worksheet.Cell("B3").Style;
                fondoTotalDiferenciaStyle.Fill.BackgroundColor = XLColor.FromHtml("#B93D7B");

                worksheet.Cell("A5").Value = "Retenciones Contabilidad";
                worksheet.Cell("B5").Value = totalContabilidad;
                worksheet.Cell("A6").Value = "Retenciones que no están en Contabilidad pero sí en AFIP";
                worksheet.Cell("B6").Value = totalRojoAfip;
                worksheet.Cell("A7").Value = "Total";
                worksheet.Cell("B7").FormulaA1 = "B5 + B6";

                // Aplicar estilo al fondo de las celdas de total y diferencia
                fondoTotalDiferenciaStyle = worksheet.Cell("B5").Style;
                fondoTotalDiferenciaStyle.Fill.BackgroundColor = XLColor.FromHtml("#B93D7B");
                fondoTotalDiferenciaStyle = worksheet.Cell("B6").Style;
                fondoTotalDiferenciaStyle.Fill.BackgroundColor = XLColor.FromHtml("#B93D7B");
                fondoTotalDiferenciaStyle = worksheet.Cell("B7").Style;
                fondoTotalDiferenciaStyle.Fill.BackgroundColor = XLColor.FromHtml("#B93D7B");

                // Diferencia
                worksheet.Cell("A9").Value = "Diferencia";
                worksheet.Cell("B9").FormulaA1 = "B3 - B7";

                // Aplicar estilo al fondo de las celdas de total y diferencia
                fondoTotalDiferenciaStyle = worksheet.Cell("B9").Style;
                fondoTotalDiferenciaStyle.Fill.BackgroundColor = XLColor.FromHtml("#B93D7B");

                fondoTotalDiferenciaStyle = worksheet.Cell("A4").Style;
                fondoTotalDiferenciaStyle.Fill.BackgroundColor = XLColor.FromHtml("#B93D7B");
                fondoTotalDiferenciaStyle = worksheet.Cell("B4").Style;
                fondoTotalDiferenciaStyle.Fill.BackgroundColor = XLColor.FromHtml("#B93D7B");
                fondoTotalDiferenciaStyle = worksheet.Cell("A8").Style;
                fondoTotalDiferenciaStyle.Fill.BackgroundColor = XLColor.FromHtml("#B93D7B");
                fondoTotalDiferenciaStyle = worksheet.Cell("B8").Style;
                fondoTotalDiferenciaStyle.Fill.BackgroundColor = XLColor.FromHtml("#B93D7B");

                // Resaltar todos los bordes
                var bordesStyle = worksheet.RangeUsed().Style.Border;
                bordesStyle.BottomBorder = XLBorderStyleValues.Thin;
                bordesStyle.TopBorder = XLBorderStyleValues.Thin;
                bordesStyle.LeftBorder = XLBorderStyleValues.Thin;
                bordesStyle.RightBorder = XLBorderStyleValues.Thin;

                // Ajustar automáticamente el ancho de las columnas
                worksheet.Columns().AdjustToContents();

                // Guardar el archivo
                workbook.SaveAs(rutaArchivo);
            }
        }

        // Función auxiliar para obtener el índice de la columna por su nombre
        static int ObtenerIndiceColumna(IXLWorksheet worksheet, string nombreColumna)
        {
            int indiceColumna = -1;

            for (int col = 1; col <= worksheet.LastColumnUsed().ColumnNumber(); col++)
            {
                string valor = worksheet.Cell(1, col).GetString();

                if (valor.Equals(nombreColumna, StringComparison.OrdinalIgnoreCase))
                {
                    indiceColumna = col;
                    break;
                }
            }

            return indiceColumna;
        }

        private void textBoxReporte_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxLogoEstudio_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InicializarYMostrarEsquemas()
        {
            // Borra los elementos existentes en el ComboBox
            comboBoxEsquemas.Items.Clear();

            // Define una lista de esquemas
            List<Esquema> listaEsquemas = new List<Esquema>();

            // Ruta del archivo Esquemas en el directorio de la aplicación
            string filePath = Path.Combine(Application.StartupPath, "Esquemas.txt");

            // Cargar los esquemas desde el archivo
            CargarEsquemasDesdeArchivo(filePath, listaEsquemas);

            // Agregar los nombres de los esquemas al ComboBox
            foreach (Esquema esquema in listaEsquemas)
            {
                comboBoxEsquemas.Items.Add(esquema.Nombre);
            }

            // Mostrar el primer esquema en el ComboBox si hay al menos uno
            if (comboBoxEsquemas.Items.Count > 0)
            {
                comboBoxEsquemas.SelectedIndex = 0;
            }

            buttonEditarEsquema.Visible = true;
        }

        private void CargarEsquemasDesdeArchivo(string filePath, List<Esquema> listaEsquemas)
        {
            try
            {
                // Leer todas las líneas del archivo
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    // Ignorar las líneas en blanco o nulas
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    try
                    {
                        // Deserializar cada línea del archivo en un objeto Esquema
                        Esquema esquema = Newtonsoft.Json.JsonConvert.DeserializeObject<Esquema>(line);

                        // Agregar el esquema a la lista
                        listaEsquemas.Add(esquema);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al deserializar la línea '{line}': {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los esquemas: " + ex.Message);
            }
        }

        private void buttonCrearEsquema_Click(object sender, EventArgs e)
        {
            // Abrir el formulario para definir columnas
            FormColumnas columnasForm = new FormColumnas();
            columnasForm.ShowDialog();

            InicializarYMostrarEsquemas();
        }

        private void buttonEditarEsquema_Click(object sender, EventArgs e)
        {
            // Define una lista de esquemas
            List<Esquema> listaEsquemas = new List<Esquema>();

            // Ruta del archivo Esquemas en el directorio de la aplicación
            string filePath = Path.Combine(Application.StartupPath, "Esquemas.txt");

            // Cargar los esquemas desde el archivo
            CargarEsquemasDesdeArchivo(filePath, listaEsquemas);

            FormColumnas columnasForm = new FormColumnas();

            foreach (Esquema esquema in listaEsquemas)
            {
                if (comboBoxEsquemas.SelectedItem.ToString() == esquema.Nombre)
                {
                    columnasForm.cargarDatos(esquema.IndiceCuit, esquema.IndiceFecha, esquema.IndiceCertificado, esquema.IndiceImporte, esquema.Nombre);
                }
            }

            columnasForm.ShowDialog();

            InicializarYMostrarEsquemas();
        }

        private void buttonEliminarEsquema_Click(object sender, EventArgs e)
        {
            // Ruta del archivo Esquemas en el directorio de la aplicación
            string filePath = Path.Combine(Application.StartupPath, "Esquemas.txt");

            try
            {
                // Leer los esquemas existentes
                List<Esquema> esquemasExistentes = new List<Esquema>();
                CargarEsquemasDesdeArchivo(filePath, esquemasExistentes);

                // Buscar el esquema por nombre seleccionado en el ComboBox
                if (comboBoxEsquemas.SelectedItem != null)
                {
                    var esquemaExistente = esquemasExistentes.Find(e => e.Nombre == comboBoxEsquemas.SelectedItem.ToString());

                    if (esquemaExistente != null)
                    {
                        // Confirmación para eliminar el esquema
                        DialogResult result = MessageBox.Show($"¿Está seguro de que desea eliminar el esquema '{esquemaExistente.Nombre}'?",
                                                              "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            // Eliminar el esquema de la lista
                            esquemasExistentes.Remove(esquemaExistente);

                            // Serializar la lista de esquemas a JSON y escribir en el archivo
                            List<string> esquemasJson = new List<string>();
                            foreach (var esq in esquemasExistentes)
                            {
                                esquemasJson.Add(Newtonsoft.Json.JsonConvert.SerializeObject(esq));
                            }
                            File.WriteAllLines(filePath, esquemasJson);

                            MessageBox.Show("Esquema eliminado correctamente.");

                            // Actualizar el ComboBox
                            comboBoxEsquemas.Items.Remove(esquemaExistente.Nombre);

                            // Limpiar la selección del ComboBox si ya no hay esquemas
                            if (comboBoxEsquemas.Items.Count == 0)
                            {
                                comboBoxEsquemas.SelectedIndex = -1;
                                comboBoxEsquemas.Text = "";  // Opcional: limpiar texto visible
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("El esquema no existe.");
                    }
                }
                else
                {
                    MessageBox.Show("No se ha seleccionado ningún esquema para eliminar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el esquema: " + ex.Message);
            }

            // Volver a cargar los esquemas (opcional, si la función actualiza otros elementos)
            InicializarYMostrarEsquemas();
        }

    }

    // Clase para representar un esquema
    class Esquema
    {
        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("IndiceColumnaCuit")]
        public int IndiceCuit { get; set; }

        [JsonProperty("IndiceColumnaFecha")]
        public int IndiceFecha { get; set; }

        [JsonProperty("IndiceColumnaCertificado")]
        public int IndiceCertificado { get; set; }

        [JsonProperty("IndiceColumnaImporte")]
        public int IndiceImporte { get; set; }


        public Esquema() { }

        public Esquema(int indiceCuit, int indiceFecha, int indiceCertificado, int indiceImporte)
        {
            IndiceCuit = indiceCuit;
            IndiceFecha = indiceFecha;
            IndiceCertificado = indiceCertificado;
            IndiceImporte = indiceImporte;
        }
    }
}