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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim pegawai = InputBox("Silahkan Masukan ID Karyawan")
        Try
            DS.Tables(0).PrimaryKey = New DataColumn() {DS.Tables(0).Columns("ID_Karyawan")}

            Dim row As DataRow
            row = DS.Tables(0).Rows.Find(pegawai)
            txtIdKaryawan.Text = row("ID_Karyawan")
            txtNamaKaryawan.Text = row("Nama_Karyawan")
            txtTempatLahir.Text = row("Tempat_Lahir")
            dtpLahir.Text = row("Tanggal_Lahir")
            cmbJenisKelamin.Text = row("Jenis_Kelamin")
            cmbAgama.Text = row("Agama")
            txtTelpon.Text = row("No_Telepon")
            rchAlamat.Text = row("Alamat_Karyawan")
            cmbStatus.Text = row("Status_Karyawan")
            txtNamaPhotoKaryawan.Text = row("Foto_Karyawan")
            Refresh()
            MsgBox("Pencarian Sukses !")
            Refresh()
        Catch ex As Exception
           'MsgBox(ex.ToString())
            MsgBox("Anda Salah Memasukkan ID Admin/ID Admin Tersebut Belum Terdaftar !")
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        DA = New OleDb.OleDbDataAdapter("SELECT * from Karyawan where ID_Karyawan like '%" & txtcari.Text.Replace("'", "''") & "%' or Nama_Karyawan like '%" & txtcari.Text.Replace("'", "''") & "%' or Alamat_Karyawan like '%" & txtcari.Text.Replace("'", "''") & "%' ", Conn)
        DS = New DataSet
        Dim SRT As New DataTable
        SRT.Clear()
        DA.Fill(SRT)
        DataGridView1.DataSource = SRT
    End Sub

    Private Sub dtpLahir_ValueChanged(sender As Object, e As EventArgs) Handles dtpLahir.ValueChanged
        Dim Umur, Bulan, Hari, TL, TS, Semester As Integer
        TL = Year(dtpLahir.Value)
        'Dim format As String = "dddd"
        'Debug.WriteLine(dtpLahir.Value.ToString(format))
        TS = Year(Now)
        Umur = TS - TL
        'menghitung umur
        Bulan = DateDiff(DateInterval.Month, CDate(dtpLahir.Value), CDate(Now))
        'menghitung total jumlah bulan dari DateTimePicker dan bulan sekarang
        Hari = DateDiff(DateInterval.Day, CDate(dtpLahir.Value), CDate(Now))
        Semester = DateDiff(DateInterval.Month, CDate(dtpLahir.Value), CDate(Now))

        'Select Case Semester
        '   Case 0 To 6 : txtumur.Text = 1
        '   Case 7 To 12 : txtumur.Text = 2
        '   Case 13 To 18 : txtumur.Text = 3
        '   Case 19 To 24 : txtumur.Text = 4
        '   Case 25 To 30 : txtumur.Text = 5
        '   Case 31 To 36 : txtumur.Text = 6
        '    Case 36 To 42 : txtumur.Text = 7
        'End Select
        'txtumur.Text = dtpLahir.Value.ToString(format)
        txtumur.Text = (Umur & " Tahun " & Bulan & " bulan " & Hari & " hari ")
    End Sub

    Private Sub txtIdKaryawan_TextChanged(sender As Object, e As EventArgs) Handles txtIdKaryawan.TextChanged
        Try
            CMD = New OleDb.OleDbCommand("select * from Karyawan where ID_karyawan ='" & txtIdKaryawan.Text & "'", Conn)
            DM = CMD.ExecuteReader
            'DM.Read()
            If DM.HasRows = True Then
                DM.Read()
                'Dim row As DataRow
                'row = DS.Tables(0).Rows.Find(pegawai)
                txtIdKaryawan.Text = DM.Item("id_karyawan")
                txtNamaKaryawan.Text = DM.Item("nama_karyawan")
                txtTempatLahir.Text = DM.Item("tempat_lahir")
                dtpLahir.Text = DM.Item("Tanggal_Lahir")
                cmbJenisKelamin.Text = DM.Item("Jenis_Kelamin")
                cmbAgama.Text = DM.Item("agama")
                txtTelpon.Text = DM.Item("no_telepon")
                rchAlamat.Text = DM.Item("alamat_karyawan")
                cmbStatus.Text = DM.Item("status_karyawan")
                txtNamaPhotoKaryawan.Text = DM.Item("foto_karyawan")
                PictureBox1.ImageLocation = Replace((DM("foto_karyawan")), ";", "\")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
End Class