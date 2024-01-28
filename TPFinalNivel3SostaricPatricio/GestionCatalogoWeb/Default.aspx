<%@ Page Title="" Language="C#" MasterPageFile="~/MiMasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GestionCatalogoWeb.Default1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="scriptManager1" runat="server" />
    <div class="text-center mt-3 mb-3">
        <h1 class="mb-3">Bienvenido</h1>
        <p>Llegaste a catalogo de Productos</p>
    </div>

    <div class="row">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                    <%if (ddwnFiltroCampoDefault.Text == "Precio")
                {%>
                     <asp:RegularExpressionValidator  CssClass="validacion" ErrorMessage="Solo números enteros." ControlToValidate="txbFiltroDefault" ValidationExpression="^[0-9]+([][0-9]+)?$" runat="server" />
                <% } %>
                <div class="text-center mt-3 mb-3 d-flex align-items-center justify-content-center ">
                    <%-- <% if (ConexionDb.Seguridad.sesionActiva(Session["usuario"]))
                        {
%>--%>

                    <div class="col-2">
                        <div class="mb-3">
                            <label class="Text-Black ">-Seleccione Criterios de busqueda:</label>
                        </div>
                    </div>
                    <div class="col-2 m-2">

                        <div class="mb-3">
                            <asp:DropDownList runat="server" ID="ddwnFiltroCampoDefault" AutoPostBack="true" OnSelectedIndexChanged="ddwnFiltroCampoDefault_SelectedIndexChanged" CssClass="form-control">
                                <asp:ListItem Text="Filtrar por..." />
                                <asp:ListItem Text="Nombre" />
                                <asp:ListItem Text="Categoria" />
                                <asp:ListItem Text="Precio" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-2 m-2">
                        <div class="mb-3 dropdown ">

                            <asp:DropDownList runat="server" ID="ddwnFiltroCriterioDefault" AutoPostBack="true" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-2 m-2">
                        <div class="mb-3">
                         
                            <asp:TextBox runat="server" ID="txbFiltroDefault" AutoPostBack="true" CssClass="form-control" />
                          
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="mb-2">
                            <asp:Button Text="Buscar" runat="server" ID="btnBuscarFiltroDefault" OnClick="btnBuscarFiltroDefault_Click" CssClass="btn btn-dark" />
                        </div>
                    </div>
                    <%--% } --%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

     <div class="row row-cols-1 row-cols-md-3 g-4">
   
        <%  foreach (var item in ListaProductos)
            {
        %>
        <div class="col">
                    <div class="card">
       
                <img src="<%:item.ImagenUrl %>"  class="card-img-top" alt="Imagen del articulo" Style="max-width:500px;max-height:600px;" onerror="this.src='https://www.mansor.com.uy/wp-content/uploads/2020/06/imagen-no-disponible2.jpg'">
                
                <div class="card-body bg-light">
                    <h5 class="card-title"><%:item.Nombre%></h5>
                    <%--item porque en el for each no cambie y la deje asi.--%>
                    <p class="card-text"><%:"$"+item.Precio %></p>
                    <a href="DetalleArticulo.aspx?Id=<%:item.Id %>">Ver Detalle</a>
                </div>
            </div>
        </div>
        <%  }%>
    </div>
    
</asp:Content>
