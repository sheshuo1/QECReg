<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="QECReg.register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>《挂机英雄》封测用户注册页面</title>
    <style type="text/css">
      body{ background:#828caa;margin:0; padding:0;font-family:"微软雅黑", "Adobe 黑体 Std R", "Adobe 宋体 Std L",Arial, Helvetica, sans-serif; font-size:13px; }
      #form1{position:relative;margin:0 auto;width:1431px;height:1056px;background:#828caa url(images/body.jpg) no-repeat;}
      #main{width:753px;height:363px;position: absolute;top:330px;left:420px;}
      span{display:inline-block;}
      
      .zcmb01_input01{width:360px;height:30px;margin:10px 10px 18px 5px;}
      .zcmb01txt2{width:2%;color:White;}
      .zcmb01txt3{width:40%;color:White;}
      #btnSubmit{background:url(images/btn.jpg) no-repeat}
   </style>
  
</head>
<body>
    <form id="form1" runat="server">
    <div id="main">
        <div class="input">
            
           
                <asp:TextBox  class="zcmb01_input01" ID="txtName" runat="server" MaxLength="13" ></asp:TextBox>             
           
            <span class="zcmb01txt2">*</span>
           <asp:Label class="zcmb01txt3" ID="lblNameError" runat="server" Text="输入至少3位用户名,可包含数字、字母、下划线"></asp:Label>
        </div>
        <div class="input">
            
           
            
                <asp:TextBox class="zcmb01_input01" ID="txtPassword" runat="server" TextMode="Password" MaxLength="20"></asp:TextBox>
            
            <span class="zcmb01txt2">*</span>
            <asp:Label class="zcmb01txt3" ID="lblPassError" runat="server" Text="请输入至少6位密码"></asp:Label>
        </div>
        <div class="input">
            
           
            
            <asp:TextBox  class="zcmb01_input01" ID="txtPassword2" runat="server" TextMode="Password" MaxLength="20"></asp:TextBox>
            
            <span class="zcmb01txt2">*</span>
            <asp:Label class="zcmb01txt3" ID="lblPass2Error" runat="server" Text="请重复输入"></asp:Label>
        </div>
        <div class="input">
            
            
                <asp:TextBox  class="zcmb01_input01" ID="txtmail" runat="server" MaxLength="50"></asp:TextBox>             
            
            <span class="zcmb01txt2">*</span>
            <asp:Label class="zcmb01txt3" ID="lblMailError" runat="server" Text="请输入邮箱地址,用于发放公测奖励"></asp:Label>
        </div>
        <div class="input">
           
            
                <asp:TextBox  class="zcmb01_input01" ID="txtqq" runat="server" MaxLength="15"></asp:TextBox>             
           
            <span class="zcmb01txt2">*</span>
            <asp:Label class="zcmb01txt3" ID="lblQqError" runat="server" Text="请输入QQ号"></asp:Label>
        </div>
        <div class="input">
            
            
                <asp:TextBox  class="zcmb01_input01" ID="textkod" runat="server" MaxLength="20"></asp:TextBox>             
            
            <span class="zcmb01txt2"> </span>
            <asp:Label class="zcmb01txt3" ID="lblKodError" runat="server" Text="可选填，用于发放决斗之王DP奖励"></asp:Label>
        </div>
        <div class="input">
          
            
                <asp:TextBox class="zcmb01_input01" ID="txtjihuoma" runat="server" MaxLength="7"></asp:TextBox>             
            
            <span class="zcmb01txt2">*</span>
            <asp:Label class="zcmb01txt3" ID="lblCodeError" runat="server" Text="请输入封测激活码，不区分大小写"></asp:Label>
        </div>
        <br />
        <div><asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="images/btn.png" onclick="btnSubmit_Click" /></div>
        
    </div>
    </form>
</body>
</html>
