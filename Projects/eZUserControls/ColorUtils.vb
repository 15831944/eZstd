Namespace eZstd

    ''' <summary>
    ''' 对颜色进行处理
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ColorUtils

        ''' <summary>
        ''' RGB 颜色插值
        ''' </summary>
        ''' <param name="c1">颜色1所对应的数值</param>
        ''' <param name="c2">颜色2所对应的数值</param>
        ''' <param name="x">x的值位于[0,1]的闭区间内，当其值为0时，它代表颜色c1，当其值为1时，它代表颜色c2</param>
        ''' <returns>插值后的RGB颜色所对应的数值</returns>
        ''' <remarks>返回的整数值与RGB参量的换算关系为：Color属性值=R + 256*G + 256^2*B</remarks>
        Public Shared Function ColorInterp(c1, c2, x) As Double

            Dim R0, G0, B0, R1, G1, B1 As Byte

            ' c0 的 各分量
            R0 = c1 Mod 256
            G0 = c1 \ 256 Mod 256
            B0 = c1 \ 256 \ 256

            ' c1 的 各分量
            R1 = c2 Mod 256
            G1 = c2 \ 256 Mod 256
            B1 = c2 \ 256 \ 256

            '
            Dim R As Byte, G As Byte, B As Byte
            R = R0 + (R1 - R0) * x
            G = G0 + (G1 - G0) * x
            B = B0 + (B1 - B0) * x
            '
            Return R + 256.0# * G + 256.0# * 256.0# * B
        End Function

    End Class
End Namespace