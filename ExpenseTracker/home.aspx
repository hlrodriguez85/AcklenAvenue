<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="ExpenseTracker.home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript">
         //Function to allow only numbers to textbox
         function isNumberKey(key) {
             var keycode = (key.which) ? key.which : key.keyCode;
             var phn = document.getElementById('txtPhn');
             if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
                 return false;
             }
             else {
                 if (phn.value.length < 10) {
                     return true;
                 }
                 else {
                     return false;
                 }
             }
         }
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>EXPENSE TRACKER</h1>
            <table>
                <tr><td>Purchase: </td></tr>
                <tr><td>
                     <asp:TextBox ID="PurchaseTXT" MaxLength="50" runat="server"/>
                </td></tr>
                <tr><td>Category: </td></tr>
                <tr><td><asp:DropDownList ID="CategoryDDL" runat="server">
                    <asp:ListItem Text="Groceries" />
                    <asp:ListItem Text="Public Services" />
                    <asp:ListItem Text="Housing" />
                    <asp:ListItem Text="Shopping" />
                    <asp:ListItem Text="Personal" />
                    <asp:ListItem Text="Other" />
                    </asp:DropDownList></td></tr>
                <tr><td>Amount: </td></tr>
                <tr><td><asp:TextBox ID="AmountTRX" MaxLength="50" runat="server" onkeypress="return isNumberKey(event)"/>
                </td></tr>
                <tr><td>
                    <asp:Button ID="AddTRX" runat="server" Text ="Add Purchase" OnClick="AddTRX_Click"/>
                </td></tr>
            </table>
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </div>
        <div>
            <table>
                <tr><td>
                    <h3>Total spent this month: </h3>
                </td><td>
                    <asp:Label ID="TotalLbl" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td><td>
                </td></tr>
                <tr><td>
                    <h3>Highest spending category: </h3>
                </td><td>
                    <asp:Label ID="HighestCat" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    <asp:Label ID="HighAmount" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td></tr>
            </table>
        </div>
    </form>
</body>
</html>
