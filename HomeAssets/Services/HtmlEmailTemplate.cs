using System;

namespace HomeAssets.Services
{
    public static class HtmlEmailTemplate
    {
        public static string CreateHtmlBody(string user, string message, string link, string buttonTxt)
        {
            string year = DateTime.Now.Year.ToString();
            string quo = "\"";
            return @$"<!DOCTYPE html>
<html lang={quo}es{quo}>

<head>
    <meta charset={quo}UTF-8{quo}>
    <meta name={quo}viewport{quo} content={quo}width=device-width, initial-scale=1.0{quo}>
    <title>HomeAssets</title>
</head>

<body>
    <table align={quo}center{quo} border={quo}0{quo} cellpadding={quo}0{quo} cellspacing={quo}0{quo} width={quo}600{quo} style={quo}border-collapse: collapse;{quo}>
        <tr>
            <td align={quo}center{quo} bgcolor={quo}#ffffff{quo}>
                <img src={quo}
                https://jdppaq.dm.files.1drv.com/y4mfpoHh24W82w0PRk6LWE1hlh9UkR8orQHNg_mGVrSicZIHi4Cs-5QOdot0Mq33-QdXALefdN10JahNOz8_zSM3YBKgKLvx5w8uDGItEc-Q4OTjAvjMuE2jN8exWRWmqrDGEjxnZjfUuzuKxLpfYVULTt55UOr2qTh_I_0NeOMSANVHzTNS55q2_g763ULjpKPOfETNxk-aCas-x3XnTHmkg?width=660&height=252&cropmode=none{quo}
                    width={quo}362{quo} height={quo}138{quo} style={quo}padding: 40px 0px 10px 0px;{quo} />
                <hr>
            </td>
        </tr>
        <tr>
            <td bgcolor={quo}#ffffff{quo} style={quo}padding: 20px 30px 40px 30px;{quo}>
                <table border={quo}0{quo} cellpadding={quo}0{quo} cellspacing={quo}0{quo} width={quo}100%{quo}>
                    <tr>
                        <td>
                            <h1 style={quo}color: #002b15; font-family: Arial, sans-serif; font-size: 24px;{quo}>
                                Hola, {user}:
                            </h1>
                        </td>
                    </tr>
                    <tr>
                        <td
                            style={quo}color: #002b15; padding: 20px 0 30px 0; font-family: Arial, sans-serif; font-size: 16px;{quo}>
                            {message}
                        </td>
                    </tr>
                    <tr>
                        <td align={quo}center{quo} style={quo}padding: 20px 0 30px 0;{quo}>
                            <a
                                href={quo}{link}{quo}>
                                <button
                                    style={quo}color: #ffffff; background-color: #ff0000; font-family: Arial, sans-serif;;
                                    display: inline-block; font-weight: 400; text-align: center; vertical-align: middle; cursor: pointer; -webkit-user-select: none; -moz-user-select: none; -ms-user-select: none; user-select: none; border: 1px solid transparent; padding: 0.375rem 0.75rem; font-size: 1rem; line-height: 1.5; border-radius: 0.25rem; transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;{quo}>
                                    {buttonTxt}
                                </button>
                            </a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td bgcolor={quo}#002b15{quo} style={quo}padding: 30px 30px 30px 30px;{quo}>
                <table border={quo}0{quo} cellpadding={quo}0{quo} cellspacing={quo}0{quo} width={quo}100%{quo}>
                    <tr>
                        <td style={quo}color: white; font-size: small;{quo}>
                            ©{year} HomeAssets <br>
                            <h3
                                style={quo}font-family: 'Courier New', Courier, monospace; color: #ffffff; font-size: medium;{quo}>
                                jdevops.xyz
                            </h3>
                            Has recibido este correo reglamentario para evitar la suplantación de identidad.<br>
                            Si tu no te has registrado a HomeAssets solo ignora este correo.
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>

</html>";
        }
    }
}