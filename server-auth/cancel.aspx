<%@ Page Language="C#" CodeFile="cancel.aspx.cs" Inherits="cancel" %>
<!DOCTYPE html>
<html>

<head>
  <title>Nicepay net framework</title>
  <meta httpEquiv="x-ua-compatible" content="ie=edge" />
  <meta name="viewport" content="width=device-width, initial-scale=1" />
</head>

<body>
  <h1>NICEPAY TEST</h1>
  <form method="POST" action="/cancelResponse">
    <label>tid</label><br>
    <input type="text" name="tid" value=""><br>

    <label>amount</label><br>
    <input type="text" name="amount" value="<%= amount %>"><br><br>

    <input type="submit" value="취소요청">
  </form> 
</body>

</html>