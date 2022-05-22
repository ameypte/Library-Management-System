Imports MySql.Data.MySqlClient
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32
Public Class add_book
    Dim SqlConn As New MySqlConnection
    Dim SqlCmd As New MySqlCommand
    Dim SqlRd As MySqlDataReader
    Dim SqlDt As New DataTable
    Dim DtA As New MySqlDataAdapter

    Dim server As String = "localhost"
    Dim username As String = "root"
    Dim password As String = "amey"
    Dim database As String = "library_management_system"

    Dim SqlQuery As String

    Private bitmap As Bitmap

    Private Sub updateTable()
        SqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" _
        + "password=" + password + ";" + "database=" + database

        SqlConn.Open()
        SqlCmd.Connection = SqlConn
        SqlCmd.CommandText = "SELECT * FROM library_management_system.books"
        SqlRd = SqlCmd.ExecuteReader
        SqlDt.Load(SqlRd)
        SqlRd.Close()
        SqlConn.Close()
        DataGridView1.DataSource = SqlDt
    End Sub
    Private Sub add_book_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        updateTable()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" _
        + "password=" + password + ";" + "database=" + database
        Try
            SqlConn.Open()
            SqlQuery = "Insert into library_management_system.books values(" & txtBookId.Text & ",'" & txtBookName.Text & "','" & txtAuthorName.Text & "'," & txtSerialNo.Text & "," & txtBookCategoryNo.Text & ")"
            SqlCmd = New MySqlCommand(SqlQuery, SqlConn)
            SqlRd = SqlCmd.ExecuteReader
            SqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Books", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SqlConn.Dispose()
        End Try

        updateTable()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" _
       + "password=" + password + ";" + "database=" + database

        SqlConn.Open()

        SqlCmd.Connection = SqlConn
        Dim sql As String = "update library_management_system.books set book_name = '" & txtBookName.Text & "', b_Author = '" & txtAuthorName.Text & "', serial_no = " & txtSerialNo.Text & " where book_id = " & txtBookId.Text

        Dim cmd As New MySqlCommand(sql)
        cmd.Connection = SqlConn

        cmd.ExecuteNonQuery()
        SqlConn.Close()
        updateTable()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" _
       + "password=" + password + ";" + "database=" + database
        Try
            SqlConn.Open()
            SqlQuery = "DELETE FROM library_management_system.books WHERE book_id = " & txtBookId.Text

            SqlCmd = New MySqlCommand(SqlQuery, SqlConn)
            SqlRd = SqlCmd.ExecuteReader
            SqlConn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Book", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            SqlConn.Dispose()
        End Try
        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            DataGridView1.Rows.Remove(row)
        Next
        updateTable()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        txtBookId.Enabled = False
        Try
            txtBookId.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString
            txtBookName.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString
            txtAuthorName.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString
            txtSerialNo.Text = DataGridView1.SelectedRows(0).Cells(3).Value.ToString
            txtBookCategoryNo.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        txtBookId.Enabled = True
        txtBookId.Text = ""
        txtAuthorName.Text = ""
        txtBookCategoryNo.Text = ""
        txtBookName.Text = ""
        txtSerialNo.Text = ""
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress

    End Sub
End Class