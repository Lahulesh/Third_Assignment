<%@ Page Title="" Language="C#" MasterPageFile="~/First.Master" AutoEventWireup="true" CodeBehind="newasset.aspx.cs" Inherits="Third.newasset" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:center;">
            <h1>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                New Asset</h1>
            <asp:Label ID="Label7" runat="server" Text="Id"></asp:Label>
            <asp:TextBox ID="newassetid" class="write" runat="server" autocomplete="off" readonly='true' placeholder="Auto generate Id"></asp:TextBox>
            <br />

            <asp:Label ID="Label8" runat="server" Text="Asset Name"></asp:Label>
        <asp:TextBox ID="newassetname" class="write"  runat="server" autocomplete="off" placeholder="Asset Name"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Asset Name" ControlToValidate="newassetname"></asp:RequiredFieldValidator>
            <br />

            <asp:Label ID="Label9" runat="server" Text="Vendor Name"></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select Vendor Name" ControlToValidate="DropDownList1"></asp:RequiredFieldValidator>
            <br />

            <asp:Label ID="Label10" runat="server" Text="Purchase Date"></asp:Label>
            <asp:TextBox ID="newassetpurchase" class="write"  runat="server" autocomplete="off"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Purchase Date " ControlToValidate="newassetpurchase"></asp:RequiredFieldValidator>
        
            <ajaxToolkit:CalendarExtender ID="newassetpurchase_CalendarExtender" runat="server" BehaviorID="newassetpurchase_CalendarExtender" TargetControlID="newassetpurchase" />
        
            <br />

            <asp:Label ID="Label11" runat="server" Text="Cost"></asp:Label>
            <asp:TextBox ID="newassetcost" class="write"  runat="server" type="number" autocomplete="off" placeholder="₹ Cost"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter COst" ControlToValidate="newassetcost"></asp:RequiredFieldValidator>
            <br />
            <asp:Button ID="Button1" Class="cta" runat="server" Text="Submit" OnClick="Button1_Click1" />
        <br />
    </div>
</asp:Content>
