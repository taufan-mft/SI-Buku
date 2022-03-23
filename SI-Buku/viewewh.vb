Public Class viewewh
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ReportViewer.CrystalReport21.SetParameterValue("start_date", DateTimePicker1.Value)
        ReportViewer.CrystalReport21.SetParameterValue("end_date", DateTimePicker2.Value)
        ReportViewer.Show()
    End Sub
End Class