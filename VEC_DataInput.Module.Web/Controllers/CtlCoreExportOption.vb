Imports DevExpress.Export
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.Web.Editors
Imports DevExpress.ExpressApp.Web.Editors.ASPx
Imports DevExpress.Web
Imports DevExpress.Web.Internal
Imports System
Imports System.Text
Imports System.Globalization
Imports System.Web
Imports System.Net.Mime
Imports DevExpress.XtraPrinting
Imports System.IO

' For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
Partial Public Class CtlCoreExportOption
    Inherits ViewController(Of ListView)
    Public Sub New()
        InitializeComponent()
        TargetViewType = ViewType.ListView
        ' Target required Views (via the TargetXXX properties) and create their Actions.
    End Sub
    Private expController As ExportController
    Protected Overrides Sub OnActivated()
        MyBase.OnActivated()
        expController = Frame.GetController(Of ExportController)()
        If expController IsNot Nothing Then AddHandler expController.CustomExport, AddressOf ViewController1_CustomExport
    End Sub
    Protected Overrides Sub OnViewControlsCreated()
        MyBase.OnViewControlsCreated()
        ' Access and customize the target View control.
    End Sub

    Private Sub ViewController1_CustomExport(ByVal sender As Object, ByVal e As CustomExportEventArgs)
        Dim dataAwareOptions As IDataAwareExportOptions = TryCast(e.ExportOptions, IDataAwareExportOptions)
        Try
            If e.ExportTarget.ToString.Contains("Csv") Then
                dataAwareOptions.CSVEncoding = System.Text.Encoding.Default
                dataAwareOptions.CSVSeparator = CultureInfo.CurrentCulture.TextInfo.ListSeparator.ToString()
                dataAwareOptions.DocumentCulture = CultureInfo.CurrentCulture
            End If
        Catch ex As Exception

        End Try

        If dataAwareOptions IsNot Nothing Then
            AddHandler dataAwareOptions.CustomizeCell, AddressOf Options_CustomizeCell
        End If

        Dim aspExporter = TryCast(e.Printable, ASPxGridViewExporter)
        If aspExporter IsNot Nothing Then

            aspExporter.Styles.Default.Font.Name = "Arial,8"

            AddHandler aspExporter.RenderBrick, AddressOf AspExporter_RenderBrick
        End If
    End Sub

    'Public Shared Sub WriteResponse(ByVal response As HttpResponse, ByVal filearray() As Byte, ByVal type As String)
    '    response.ClearContent()
    '    response.Buffer = True
    '    response.Cache.SetCacheability(HttpCacheability.Private)
    '    response.ContentType = "application/pdf"
    '    Dim contentDisposition As New ContentDisposition()
    '    contentDisposition.FileName = "test.pdf"
    '    contentDisposition.DispositionType = type
    '    response.AddHeader("Content-Disposition", contentDisposition.ToString())
    '    response.BinaryWrite(filearray)
    '    HttpContext.Current.ApplicationInstance.CompleteRequest()
    '    Try
    '        response.End()
    '    Catch e1 As System.Threading.ThreadAbortException
    '    End Try

    'End Sub

    Private Sub AspExporter_RenderBrick(ByVal sender As Object, ByVal e As ASPxGridViewExportRenderingEventArgs)
        If e.RowType = GridViewRowType.Data Then
            Dim gridViewDataColumn As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
            If gridViewDataColumn Is Nothing Then Return
            Dim visibleIndex = e.VisibleIndex
            Dim tempValue = GetValueFromEditor(visibleIndex, gridViewDataColumn)
            If tempValue IsNot Nothing Then e.Text = tempValue
        End If
    End Sub

    Private Sub Options_CustomizeCell(ByVal e As CustomizeCellEventArgs)
        If e.AreaType = SheetAreaType.DataArea Then
            e.Formatting.Font.Name = "tahoma"
            Dim visibleIndex = (CType(e.DataSourceOwner, GridViewExcelDataPrinter)).GetVisibleIndex(e.RowHandle)
            Dim editor = TryCast(View.Editor, ASPxGridListEditor)
            If editor Is Nothing Then Return

            Dim gridView = TryCast(editor.Control, ASPxGridView)

            If gridView Is Nothing Then Return
            Dim gridViewDataColumn As GridViewDataColumn = TryCast(gridView.Columns(e.ColumnFieldName), GridViewDataColumn)
            If gridViewDataColumn Is Nothing Then Return
            Dim tempValue = GetValueFromEditor(visibleIndex, gridViewDataColumn)
            If tempValue IsNot Nothing Then
                e.Value = tempValue
                e.Handled = True
            End If
        End If
    End Sub

    Private Function GetValueFromEditor(ByVal visibleIndex As Integer, ByVal gridViewDataColumn As GridViewDataColumn) As String
        Dim template As ViewModeDataItemTemplate = TryCast(gridViewDataColumn.DataItemTemplate, ViewModeDataItemTemplate)

        If template IsNot Nothing AndAlso template.PropertyEditor IsNot Nothing AndAlso TypeOf template.PropertyEditor Is IMySupportExportCustomValue Then
            Dim editor = TryCast(View.Editor, ASPxGridListEditor)

            Dim Grid = editor.Grid

            template.PropertyEditor.CurrentObject = Grid.GetRow(visibleIndex)
            Dim localizedValue As String = (CType(template.PropertyEditor, IMySupportExportCustomValue)).GetExportedValue()
            If localizedValue IsNot Nothing Then
                Return localizedValue
            End If
        End If

        Return Nothing
    End Function

    Protected Overrides Sub OnDeactivated()
        MyBase.OnDeactivated()
        If expController IsNot Nothing Then RemoveHandler expController.CustomExport, AddressOf ViewController1_CustomExport
    End Sub


End Class


Interface IMySupportExportCustomValue

    Function GetExportedValue() As String

End Interface