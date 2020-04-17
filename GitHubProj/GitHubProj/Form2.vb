Imports System.ComponentModel
Imports System.Data.OleDb
Public Class Trabalhos

    Public ligacao As OleDbConnection
    Public TabelaTrabalhos As DataTable

    Public dadosLigacao As String
    Public comandoLigacao As String


    Private Sub Trabalhos_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        dadosLigacao = My.Settings.BaseTrabalhosConnectionString
        ligacao = New OleDbConnection(dadosLigacao)
        ligacao.Open()
        comandoLigacao = "SELECT * FROM Trabalhos ORDER BY codigo"
        ligacao.Close()

        AtualizarGrid()



        TabTrab.Columns(0).Width = 100
        TabTrab.Columns(2).Width = 300
        TabTrab.Columns(3).Width = 150
        TabTrab.Columns(4).Width = 147


    End Sub

    Private Sub AtualizarGrid()

        dadosLigacao = My.Settings.BaseTrabalhosConnectionString
        ligacao = New OleDbConnection(dadosLigacao)
        ligacao.Open()

        comandoLigacao = "SELECT * FROM Trabalhos ORDER BY codigo"

        TabelaTrabalhos = New DataTable

        Dim adaptador As New OleDbDataAdapter(comandoLigacao, ligacao)
        adaptador.Fill(TabelaTrabalhos)
        adaptador.Dispose()
        TabTrab.DataSource = TabelaTrabalhos.DefaultView
        ligacao.Close()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click



        Dim cmdSql As String

        cmdSql = "INSERT INTO Trabalhos (Codigo, Disciplina, Descricao, Prazo, Estado) VALUES ('" & txtCodigo.Text & "','" & txtDisciplina.Text & "', '" &
            txtDescricao.Text & "', #" &
            DataPick.Value.ToString("dd/MM/yyyy") & "#, '" &
            cbEstado.SelectedItem & "')"
        GerirLigacao.ExecutarCmdSQL(cmdSql)

        cmdSql = "SELECT * FROM Trabalhos ORDER BY Codigo"

        GerirLigacao.ExecutarCmdSQL(cmdSql)



        AtualizarGrid()
        Limpar()


    End Sub

    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click
        Me.Close()
    End Sub

    Private Sub Limpar()
        txtCodigo.ResetText()
        txtDescricao.ResetText()
        txtDisciplina.ResetText()
        cbEstado.ResetText()
        DataPick.Value = Now
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

            Dim cmdSql = "DELETE FROM Trabalhos WHERE Codigo =" &
                txtCodigo.Text

            GerirLigacao.ExecutarCmdSQL(cmdSql)
            AtualizarGrid()
            Limpar()
            TabTrab.DataSource = TabelaTrabalhos.DefaultView
        End If
        AtualizarGrid()
        Limpar()


    End Sub




End Class