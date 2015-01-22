Imports System.Reflection
Imports System.ComponentModel

Public Class ctlAplicacao

    'Recebe o valor da enumeração e retorna a sua descrição, se não houver descrição retorna o valor informado
    'Exemplo: Se a descrição é OS Parceiro e o valor OSParceiro, ao passar como parâmetro OSParceiro é retornado OS Parceiro
    Public Shared Function GetEnumDescription(ByVal value As [Enum]) As String
        Dim fi As FieldInfo = value.[GetType]().GetField(value.ToString())
        Dim attributes As DescriptionAttribute() = DirectCast(fi.GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())
        If attributes IsNot Nothing AndAlso attributes.Length > 0 Then
            Return attributes(0).Description
        Else
            Return value.ToString()
        End If
    End Function

    Public Shared Function Ordenar(ByVal dt As DataTable, ByVal expressao As String, ByVal direcao As String) As DataTable
        If dt IsNot Nothing Then
            If Not String.IsNullOrEmpty(expressao) Then
                dt.DefaultView.Sort = Convert.ToString(expressao) & " " & Convert.ToString(direcao)
            End If
            Return dt
        End If
        Return Nothing
    End Function

End Class
