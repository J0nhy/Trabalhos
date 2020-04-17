Imports System.Data.OleDb

Public Class GerirLigacao

    Private Shared ligacao As New OleDbConnection

    Public Shared Sub IniciarLigacao(ByVal DadosLigacao As String)

        ligacao = New OleDbConnection(DadosLigacao)
        ligacao.Open()

    End Sub

    Public Shared Sub ExecutarCmdSQL(ByVal comando As String)



        Try
            Dim cmdSql As New OleDbCommand(comando, ligacao)
            cmdSql.ExecuteNonQuery()
        Catch ex As Exception
            Dim msg = "Aconteceu um erro de execução." & vbNewLine
            Dim botoes = MessageBoxButtons.OK
            Dim icone = MessageBoxIcon.Error
            MessageBox.Show(msg & ex.Message, "ERRO", botoes, icone)
        End Try
    End Sub

    Public Shared Function obterDados(ByVal comando As String) As DataSet

        Dim resultado As New DataSet

        Try
            Dim adaptador As New OleDbDataAdapter(comando, ligacao)
            adaptador.Fill(resultado)
            adaptador.Dispose()
        Catch ex As Exception
            Dim msg = "Aconteceu um erro ao obter dados." & vbNewLine
            Dim botoes = MessageBoxButtons.OK
            Dim icone = MessageBoxIcon.Error
            MessageBox.Show(msg & ex.Message, "ERRO", botoes, icone)


        End Try
        Return resultado
    End Function

    Public Shared Sub atualizarGrelha(ByVal comando As String,
                                      ByVal grid As DataGridView)

        Dim resultado As New DataTable()
        Try
            Dim adaptador As New OleDbDataAdapter(comando, ligacao)
            adaptador.Fill(resultado)
            adaptador.Dispose()
            grid.DataSource = resultado.DefaultView
        Catch ex As Exception
            Dim msg = "Aconteceu um erro ao exibir dados." & vbNewLine
            Dim botoes = MessageBoxButtons.OK
            Dim icone = MessageBoxIcon.Error
            MessageBox.Show(msg & ex.Message, "ERRO", botoes, icone)
        End Try

    End Sub

End Class
