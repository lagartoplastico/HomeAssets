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
<table align={quo}center{quo} border={quo}0{quo} cellpadding={quo}0{quo} cellspacing={quo}0{quo} width={quo}700{quo} style={quo}border-collapse: collapse;{quo}>
<tr><td bgcolor={quo}#ffffff{quo}>
<img src={quo}https://jdqmmq.dm.files.1drv.com/y4mNcsFxnwBjUlOQelDUL-8faGuWNgu-F1krW_BFeHkMmj2NG_sH3eGv8pNJdhoIPKXxUvjpmHsy_qt-eAXgXC4FADrsn3_5G8y4C0uCo0PVwKHKkS5gCtrHCYoIi93G331zQPN4AeSWsVYTideRmUKtMBRN-oO8ykiARzajUoEAZkROtzXSv3tyB72QPE0ua5R8_pkcEyh2CPZ5_udPqynKQ?width=236&height=90&cropmode=none{quo}
style={quo}padding: 7px 0px 2px 0px;{quo} />
<hr></td></tr>
<tr><td bgcolor={quo}#ffffff{quo} style={quo}padding: 5px 20px 20px 20px;{quo}>
<table border={quo}0{quo} cellpadding={quo}0{quo} cellspacing={quo}0{quo} width={quo}100%{quo}>
<tr><td><h1 style={quo}color: #002b15; font-family: Arial, sans-serif; font-size: 24px;{quo}>
Hola, {user}:
</h1></td></tr>
<tr><td style={quo}color: #002b15; padding: 20px 0 30px 0; font-family: Arial, sans-serif; font-size: 20px;{quo}>
{message}</td></tr>
<tr><td align={quo}center{quo} style={quo}padding: 10px 0 10px 0;{quo}>
<a href={quo}{link}{quo}>
<button style={quo}color: #ffffff; background-color: #ff0000; font-family: Arial, sans-serif; display: inline-block; font-weight: 400; text-align: center; vertical-align: middle; cursor: pointer; -webkit-user-select: none; -moz-user-select: none; -ms-user-select: none; user-select: none; border: 1px solid transparent; padding: 0.375rem 0.75rem; font-size: 1rem; line-height: 1.5; border-radius: 0.25rem; transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;{quo}>
{buttonTxt}</button>
</a></td></tr>
</table>
</td></tr><tr><td bgcolor={quo}#002b15{quo} style={quo}padding: 20px 20px 20px 20px;{quo}>
<table border={quo}0{quo} cellpadding={quo}0{quo} cellspacing={quo}0{quo} width={quo}100%{quo}>
<tr><td style={quo}color: white; font-size: small;{quo}>
©{year} HomeAssets <br>
Has recibido este correo reglamentario para evitar la suplantación de identidad.<br>
Si tu no te has registrado a HomeAssets solo ignora este correo.
</td></tr></table></td></tr></table></body></html>";
        }
    }
}