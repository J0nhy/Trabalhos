Imports System.ComponentModel
Imports System.Data.OleDb
Public Class Utilizadores

    Public ligacao As OleDbConnection
    Public tabelaUsuario As DataTable

    Public dadosLigacao As String
    Public comandoLigacao As String


    Private Sub Utilizadores_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        dadosLigacao = My.Settings.BaseTrabalhosConnectionString
        ligacao = New OleDbConnection(dadosLigacao)
        ligacao.Open()
        ligacao.Close()

        AtualizarGrid()


        TabUsuario.Columns(0).Width = 100
        TabUsuario.Columns(1).Width = 120
        TabUsuario.Columns(2).Width = 300
        TabUsuario.Columns(3).Width = 150
        TabUsuario.Columns(4).Width = 126

    End Sub

    Private Sub AtualizarGrid()

        dadosLigacao = My.Settings.BaseTrabalhosConnectionString
        ligacao = New OleDbConnection(dadosLigacao)
        ligacao.Open()

        comandoLigacao = "SELECT * FROM Usuario ORDER BY ID"

        tabelaUsuario = New DataTable

        Dim adaptador As New OleDbDataAdapter(comandoLigacao, ligacao)
        adaptador.Fill(tabelaUsuario)
        adaptador.Dispose()
        TabUsuario.DataSource = tabelaUsuario.DefaultView
        ligacao.Close()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Dim cmdSql As String

        cmdSql = "INSERT INTO Usuario (Nome, Login, Password, Nivel)" & " VALUES ('" & txtNome.Text & "', '" &
            txtLogin.Text & "', '" &
            txtPass.Text & "' , '" &
            cbNivel.SelectedItem & "')"

        GerirLigacao.ExecutarCmdSQL(cmdSql)

        cmdSql = "SELECT * FROM Usuario ORDER BY ID"

        GerirLigacao.ExecutarCmdSQL(cmdSql)

        AtualizarGrid()
        Limpar()

    End Sub

    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click
        Me.Close()
    End Sub

    Private Sub Limpar()
        txtID.ResetText()
        txtLogin.ResetText()
        txtNome.ResetText()
        cbNivel.ResetText()
        txtPass.ResetText()
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        AtualizarGrid()
        Limpar()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

        Dim msg = "Confirma a eliminação deste registo de Fatura?"
        Dim titulo = "Eliminar Fatura"
        Dim botoes = MessageBoxButtons.YesNo
        Dim icone = MessageBoxIcon.Question
        AtualizarGrid()

        If MessageBox.Show(msg, titulo, botoes, icone) = DialogResult.Yes Then

            Dim cmdSql = "DELETE FROM Usuario WHERE ID =" &
                txtID.Text

            GerirLigacao.ExecutarCmdSQL(cmdSql)
            AtualizarGrid()
            Limpar()
            TabUsuario.DataSource = tabelaUsuario.DefaultView
        End If
        AtualizarGrid()
        Limpar()



    End Sub


End Class