<%@ Page Language="C#" CodeFile="index.aspx.cs" Inherits="index" %>
<!DOCTYPE html>
<html>

<head>
  <title>Nicepay net framework</title>
  <meta httpEquiv="x-ua-compatible" content="ie=edge" />
  <meta name="viewport" content="width=device-width, initial-scale=1" />
</head>

<body>
  <h1>NICEPAY TEST</h1>
  <button onclick="clientAuth()">clientAuth �����ϱ�</button>

  <script src="https://pay.nicepay.co.kr/v1/js/"></script> 

  <script>
      function clientAuth() {
          AUTHNICE.requestPay({
            clientId: 'S1_6eaa0db1afdc41f3becb770878d67d25',
            method: 'card',
            orderId: '<%= orderId %>',
            amount: 1004,
            vbankHolder: "���̽�",
            goodsName: '���̽�����-��ǰ',
            returnUrl: 'http://localhost:8080/response',
            fnError: function (result) {
              alert('������Ȯ�ο� : ' + result.errorMsg + '')
            }
      });
      }
  </script>
</body>

</html>