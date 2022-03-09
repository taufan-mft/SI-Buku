Public Class DataKaryawan
    Dim OpenFileDialog1 As New OpenFileDialog
    Sub tampilkanData()
        DA = New OleDb.OleDbDataAdapter("SELECT * FROM karyawan", Conn)
        DS = New DataSet()
        DA.Fill(DS)
        DataGridView1.DataSource = DS.Tables(0)
        DataGridView1.ReadOnly = True
    End Sub
    Private Sub DataKaryawan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampilkanData()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If txtIdKaryawan.Text = "" Or txtNamaKaryawan.Text = "" Or txtTempatLahir.Text = "" Or cmbJenisKelamin.Text = "" Or cmbAgama.Text = "" Or txtTelpon.Text = "" Or rchAlamat.Text = "" Or cmbStatus.Text = "" Or txtNamaPhotoKaryawan.Text = "" Then
            MsgBox("Data karyawan belum lengkap!")
            Exit Sub

        Else
            CMD = New OleDb.OleDbCommand($"SELECT * FROM karyawan WHERE id_karyawan ='{txtIdKaryawan.Text}'", Conn)
            DM = CMD.ExecuteReader
            DM.Read()
            If Not DM.HasRows Then
                Dim simpan As String
                simpan = $"INSERT INTO karyawan values ('{txtIdKaryawan.Text}', 
                            '{txtNamaKaryawan.Text}', '{txtTempatLahir.Text}', '{dtpLahir.Value.ToString()}', '{cmbJenisKelamin.Text}',
                            '{cmbAgama.Text}', '{txtTelpon.Text}', '{rchAlamat.Text}', '{cmbStatus.Text}', '{txtNamaPhotoKaryawan.Text}')"
                CMD = New OleDb.OleDbCommand(simpan, Conn)
                CMD.ExecuteNonQuery()
                MsgBox("Input data sukses")
                PictureBox1.ImageLocation = ""
                tampilkanData()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            OpenFileDialog1.Filter = " Image File (*.jpeg;*jpg;*.png;*.bmp;*.gif)| *.jpeg;*jpg;*.png;*.bmp;*.gif"

            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                PictureBox1.Image = New Bitmap(OpenFileDialog1.FileName)
                txtNamaPhotoKaryawan.Text = OpenFileDialog1.FileName

            End If

        Catch ex As Exception
            Throw New ApplicationException("Gambar Gagal Masuk")

        End Try
    End Sub
End Class