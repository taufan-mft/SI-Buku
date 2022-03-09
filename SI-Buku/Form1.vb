Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksiDB()
        MatikanForm()
        TampilkanData()
    End Sub

    Sub KosongkanForm()
        txtkodebuku.Text = ""
        txtjudulbuku.Text = ""
        txtissbn.Text = ""
        txthargabuku.Text = ""
        txtstokbuku.Text = ""
        txtkodebuku.Focus()
        'Coding diatas ada untuk mengosongkan Form yang sudah kita buat , jadi ketika form pertama kali di load kondisi form sudah dalam keadaan kosong
    End Sub
    Sub MatikanForm()
        txtkodebuku.Enabled = False
        txtjudulbuku.Enabled = False
        txtissbn.Enabled = False
        txthargabuku.Enabled = False
        txtstokbuku.Enabled = False
        cmjenisbuku.Enabled = False
        cmblokasibuku.Enabled = False
        'Coding diatas untuk menutup atau mematikan form agar tidak dapat isi
    End Sub
    Sub HidupkanForm()
        txtkodebuku.Enabled = True
        txtjudulbuku.Enabled = True
        txtissbn.Enabled = True
        txthargabuku.Enabled = True
        txtstokbuku.Enabled = True
        cmjenisbuku.Enabled = True
        cmblokasibuku.Enabled = True
        'Coding diatas untuk mulai menghidupkan form agar form bisa diisi
    End Sub
    Sub TampilkanData()
        Call koneksiDB()
        DA = New OleDb.OleDbDataAdapter("select * from buku ", Conn)
        DS = New DataSet
        DA.Fill(DS)
        DGV.DataSource = DS.Tables(0)
        DGV.ReadOnly = True
        'Coding diatas untuk menampilkan data dari Ms.Access yang sudah kita buat pada Data Grid View didalam form
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        HidupkanForm()
        KosongkanForm()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        MatikanForm()
        KosongkanForm()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If txtkodebuku.Text = "" Or txthargabuku.Text = "" Or txtissbn.Text = "" Or txtjudulbuku.Text = "" Or txtstokbuku.Text = "" Or cmjenisbuku.Text = "" Or cmblokasibuku.Text = "" Then
            MsgBox("Data Buku Belum Lengkap")
            Exit Sub
            'Coding ini berfungsi untuk mengecek jika salah satu field data ada yang kosong , atau belum diisi , maka akan tampil pesan bahwa data yang di input belum lengkap
        Else
            Call koneksiDB()
            CMD = New OleDb.OleDbCommand(" select * from buku where nomor='" & txtkodebuku.Text & "'", Conn)
            DM = CMD.ExecuteReader
            DM.Read()
            'Coding ini berfungsi untuk mengecek jika di bagian kode buku (primary key) ada inputan data yang sama , maka ke Else : data sudah ada
            'Jika data inputan tidak sama , maka ke If Not : masukkan inputan dari form ke database Ms.Access
            If Not DM.HasRows Then
                Call koneksiDB()
                Dim simpan As String
                simpan = "insert into buku values ('" & txtkodebuku.Text & "', '" & txtjudulbuku.Text & "', '" & txthargabuku.Text & "','" & txtissbn.Text & "','" & txtstokbuku.Text & "','" & cmjenisbuku.Text & "','" & cmblokasibuku.Text & "')"
                CMD = New OleDb.OleDbCommand(simpan, Conn)
                CMD.ExecuteNonQuery()
                MsgBox("Input Data Sukses")
            Else
                MsgBox("Data Sudah Ada")
            End If
            Call MatikanForm()
            Call KosongkanForm()
            Call TampilkanData()
        End If
    End Sub

    Private Sub DGV_MouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV.CellClick
        On Error Resume Next
        txtkodebuku.Text = DGV.Rows(e.RowIndex).Cells(0).Value
        txtjudulbuku.Text = DGV.Rows(e.RowIndex).Cells(1).Value
        txtissbn.Text = DGV.Rows(e.RowIndex).Cells(2).Value
        txthargabuku.Text = DGV.Rows(e.RowIndex).Cells(3).Value
        txtstokbuku.Text = DGV.Rows(e.RowIndex).Cells(4).Value
        cmjenisbuku.Text = DGV.Rows(e.RowIndex).Cells(5).Value
        cmblokasibuku.Text = DGV.Rows(e.RowIndex).Cells(6).Value
        Call HidupkanForm()
        txtkodebuku.Enabled = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If txtkodebuku.Text = "" Or txthargabuku.Text = "" Or txtissbn.Text =
"" Or txtjudulbuku.Text = "" Or txtstokbuku.Text = "" Or cmjenisbuku.Text =
"" Or cmblokasibuku.Text = "" Then
            MsgBox("Data Buku Belum Lengkap")
            Exit Sub
        Else
            Call koneksiDB()
            CMD = New OleDb.OleDbCommand(" update buku set  Nama_buku = '" &
txtjudulbuku.Text & "', Harga ='" & txthargabuku.Text & "',ISSBN = '" &
txtissbn.Text & "',Stok = '" & txtstokbuku.Text & "',Jenis_buku= '" &
cmjenisbuku.Text & "',Lokasi = '" & cmblokasibuku.Text & "' where nomor
   ='" & txtkodebuku.Text & "'", Conn)
            DM = CMD.ExecuteReader
            MsgBox("Update Data Berhasil")
        End If
        Call KosongkanForm()
        Call MatikanForm()
        Call TampilkanData()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If txtkodebuku.Text = "" Then
            MsgBox("Tidak ada data yang dipilih")
            Exit Sub
        Else
            If MessageBox.Show("Are you sure to delete this data ? ",
"Konfirmasi", MessageBoxButtons.YesNoCancel) = Windows.Forms.DialogResult.Yes Then

                Call koneksiDB()
                CMD = New OleDb.OleDbCommand(" delete from buku where
nomor ='" & txtkodebuku.Text & "'", Conn)
                DM = CMD.ExecuteReader
                MsgBox("Hapus Data Berhasil")
                Call MatikanForm()
                Call KosongkanForm()
                Call TampilkanData()
            Else
                Call KosongkanForm()
                Call TampilkanData()
            End If
        End If
    End Sub
End Class
