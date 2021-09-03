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
  <button onclick="clientAuth()">clientAuth 결제하기</button>

  <script src="https://pay.nicepay.co.kr/v1/js/pay/"></script> 

  <script>
      function clientAuth() {
          PAYNICE.requestPay({
            clientId: '클라이언트 키',
            method: 'card',
            orderId: '<%= orderId %>',
            amount: 1004,
            vbankHolder: "나이스",
            goodsName: '나이스페이-상품',
            returnUrl: 'http://localhost:8080/response',
            fnError: function (result) {
              alert('고객용메시지 : ' + result.msg + '\n개발자확인용 : ' + result.errorMsg + '')
            }
      });
      }
  </script>
</body>

</html>