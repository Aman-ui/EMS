<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadTickets.aspx.cs" Inherits="Timesheetmanagement.UploadTickets" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width:100%">
        <tr>
            <td align="left">
                 <div>
            <asp:LinkButton runat="server" ID="BtnBack" Text="<< Back" PostBackUrl="~/Calender.aspx" CausesValidation="false"></asp:LinkButton>
        </div>
            </td>
             
            <td align="right">
                <asp:ImageButton ID="ImgDownloadExcel" Height="30px" ImageUrl="~/Images/Excel-logo.gif"
                    runat="server" OnClick="ImgDownloadExcel_Click" />
            </td>
        </tr>
        <tr>
            <td align="center" valign="top">
                <fieldset>
                    <legend class="LabelText"></legend>
                    <fieldset>
                        <legend class="LabelText">Upload Panel</legend>
                        <table align="left">
                            <tr>
                                <td>
                                    <asp:Label runat="Server" CssClass="LabelText" ID="LblExcelFile" Text="Select Excel File :"></asp:Label>
                                </td>
                                <td>
                                    <asp:FileUpload runat="server" ID="FuexcelFile" CssClass="BrowseFileds" />
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="BtnExcelUpload" CssClass="Btn" Text="Upload" OnClick="BtnExcelUpload_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </fieldset>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <fieldset>
                    <legend class="LabelText">Data</legend>
                    <table align="left" style="width:100%">
                        <tr>
                            <td>
                                <asp:GridView runat="server" ID="GrdUploadedData" AutoGenerateColumns="false"  Width="100%">
                                    <Columns>
                                        
                                        <asp:BoundField DataField="TicketNumber" HeaderText="Ticket Number" />
                                        <asp:BoundField DataField="TicketApplicationName" HeaderText="Application Name" />
                                        <asp:BoundField DataField="TicketDescription" HeaderText="Description" />
                                         <asp:BoundField DataField="TicketType" HeaderText="Ticket Type" />
                                        <asp:BoundField DataField="TicketStatus" HeaderText="Status" />
                                        <asp:BoundField DataField="TICKETCREATEDON" HeaderText="Created on" />
                                        <asp:BoundField DataField="MESSAGE" HeaderText="Message" />
                                       
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
