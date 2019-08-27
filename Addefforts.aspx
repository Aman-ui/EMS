<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Addefforts.aspx.cs" Inherits="Timesheetmanagement.Addefforts" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>:::: Time Sheet Management ::::</title>
    <link href="Content/efforts.css" rel="stylesheet"  media="all"/>
    <script  type="text/javascript" >
    
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
          && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
   
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"
            EnablePageMethods="true">
        </asp:ScriptManager>
        <div>
            <asp:LinkButton runat="server" ID="BtnBack" Text="<< Back" PostBackUrl="~/Calender.aspx" CausesValidation="false"></asp:LinkButton>
            <asp:Label runat="server" ID="LblPagemode" Visible="false"></asp:Label>
        </div>
        <div>


            <asp:Repeater runat="server" ID="RptAddefforts" OnItemDataBound="RptAddefforts_ItemDataBound" OnItemCommand="RptAddefforts_ItemCommand"  >
                <HeaderTemplate>
                    <table style="width:100%;" class="repeaterborder">
                        <thead >
                            <tr style="background-color:gray; color:white">
                                <td class="repeaterheaderlabel">Ticket Number
                                </td>
                                <td class="repeaterheaderlabel">Ticket type
                                </td>
                                <td class="repeaterheaderlabel">Application
                                </td>
                                <td class="repeaterheaderlabel">Application Type
                                </td>
                                <%--<td class="repeaterheaderlabel">Vendor
                                </td>--%>
                                <td class="repeaterheaderlabel">Classification
                                </td>
                                <td class="repeaterheaderlabel">Description
                                </td>
                                <%--<td class="repeaterheaderlabel"> Status
                                </td>--%>
                                <td class="repeaterheaderlabel">Created on
                                </td>

                                <td class="repeaterheaderlabel">Remarks
                                </td>
                                <td class="repeaterheaderlabel">Efforts in <br /> minutes
                                </td>
                                <td class="repeaterheaderlabel">

                                </td>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr >
                            <td colspan="12" style="border-bottom: 1px solid black">

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" ID="TxtTicketNumber" Text='<%# DataBinder.Eval(Container.DataItem,"TicketNumber") %>' OnTextChanged="TxtTicketNumber_Changed" AutoPostBack="true" CssClass="repeateritemlabel"></asp:TextBox>
                                <cc1:AutoCompleteExtender ServiceMethod="searchTickets"
                                    MinimumPrefixLength="2"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                    TargetControlID="TxtTicketNumber"
                                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                </cc1:AutoCompleteExtender>
                                <asp:Label runat="server" ID="LblTEID" Text='<%# DataBinder.Eval(Container.DataItem,"TEID")%>' Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="LblTID" Text='<%# DataBinder.Eval(Container.DataItem,"TID") %>' Visible="false"></asp:Label>
                                <asp:RequiredFieldValidator runat="server" ID="ReqTicketnumber" ControlToValidate="TxtTicketNumber" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="DrpTicketType" CssClass="repeateritemlabel" OnSelectedIndexChanged="DrpTicketType_SelectedIndexChanged"  AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="ReqTicketType" ForeColor="Red" ControlToValidate="DrpTicketType" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="DrpApplications" CssClass="repeateritemlabel" OnSelectedIndexChanged="DrpApplications_SelectedIndexChanged" AutoPostBack="true" >
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator runat="server" ID="ReqApplications" ForeColor="Red" ControlToValidate="DrpApplications" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="DrpApplicationType" CssClass="repeateritemlabel" Enabled="false" >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="ReqAppType" ForeColor="Red" ControlToValidate="DrpApplicationType" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <%--<td>
                                <asp:DropDownList runat="server" ID="DrpVendor" CssClass="repeateritemlabel"  Enabled="false" >
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator runat="server" ID="ReqVendor" ForeColor="Red" ControlToValidate="DrpVendor" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>--%>
                            <td>
                                <asp:DropDownList runat="server" ID="DrpClassification" CssClass="repeateritemlabel" >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="Reqclassifications" ForeColor="Red" ControlToValidate="DrpClassification" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="LblDescription" Text='<%#DataBinder.Eval(Container.DataItem,"TicketDescription") %>' CssClass="repeateritemlabel"  ></asp:Label>
                            </td>
                            
                            <%--<td>
                                <asp:DropDownList runat="server" ID="DrpTicketStatus" CssClass="repeateritemlabel" >
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator runat="server" ID="ReqDrpTicketstatus" ForeColor="Red" ControlToValidate="DrpTicketStatus" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>--%>
                            <td>
                                <asp:Label runat="server" ID="LblTicketcreatedon" Text='<%#DataBinder.Eval(Container.DataItem,"TECreatedOn") %>' CssClass="repeateritemlabel"></asp:Label>
                            </td>

                            <td>
                                <asp:TextBox runat="server" ID="TxtRemarks" Text='<%#DataBinder.Eval(Container.DataItem,"Remarks") %>' CssClass="repeateritemlabel" TextMode="MultiLine" MaxLength="2000"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TxtEfforts" Text='<%# DataBinder.Eval(Container.DataItem,"TEfforts") %>' CssClass="repeateritemlabel" MaxLength="4" Width="30" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="ReqEfforts" ControlToValidate="TxtEfforts" ErrorMessage="*" ForeColor="Red" ></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Button runat="server" ID="Btndelete" Text="X" ForeColor="Red" CssClass="repeateritemlabel" CausesValidation="false" CommandName="delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"TEID")%>' /><%--Visible="false" />--%>
                            </td>
                        </tr>

                    </tbody>
                </ItemTemplate>
                <FooterTemplate>
                    <tfooter>
                    <tr>
                        <td colspan="12">
                            <asp:Button runat="server" ID="BtnAdd" Text="Add More" OnClick="AddRow" CausesValidation="false" />

                            <asp:Button runat="server" ID="BtnSave" Text="Save" OnClick="UpdateData" CausesValidation="true" />
                        </td>
                    </tr>

                    </table>
                        </tfooter>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
