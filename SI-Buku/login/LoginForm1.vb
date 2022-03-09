Public Class LoginForm1


    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click

        Dim sql As String 'gaperlu pake symbol &
        sql = $"Select * FROM Karyawan WHERE Nama_Karyawan='{UsernameTextBox.Text}' AND ID_Karyawan='{PasswordTextBox.Text}'"
        CMD = New OleDb.OleDbCommand(sql, Conn)
        CMD.ExecuteNonQuery()
        DM = CMD.ExecuteReader
        If DM.HasRows = True Then
            DM.Read()
            DataKaryawan.lblNama.Text = $"Selamat datang {UsernameTextBox.Text}"
            DataKaryawan.Show()
            Me.Hide()
        Else
            MsgBox(" Maaf Username atau Password Anda Salah ")
            Me.Show()
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub LoginForm1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksiDB()
    End Sub
End Class
