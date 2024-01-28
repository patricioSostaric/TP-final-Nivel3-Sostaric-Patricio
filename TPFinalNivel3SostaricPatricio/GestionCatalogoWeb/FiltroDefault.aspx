<%@ Page Title="" Language="C#" MasterPageFile="~/MiMasterPage.Master" AutoEventWireup="true" CodeBehind="FiltroDefault.aspx.cs" Inherits="GestionCatalogoWeb.FiltroDefault" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="text-center mt-3 mb-3">
        <div class="mb-3">
            <h1>Filtro Aplicado</h1>
            <%if (MostrarFiltro.Count != 0)
                { %>
            <p>Productos que coinciden con tu busqueda.</p>
            <%} %>
            <%else
                { %>
            <p>No hay productos que coincidan con tu busqueda.</p>
            <%} %>
        </div>
        <div class="mb-3">
            <asp:Button Text="Volver" runat="server" ID="btnVolverFiltroDefault" OnClick="btnVolverFiltroDefault_Click" CssClass="btn btn-dark" />
        </div>
    </div>
     <div class="row row-cols-1 row-cols-md-3 g-4">
    
        <%  foreach (var item in MostrarFiltro)
            {
        %>
        <div class="col">
                    <div class="card">
        
                <img src="<%:item.ImagenUrl %>" class="card-img-top" alt="Imagen del articulo" Style="max-width:500px;max-height:600px;" onerror="this.src='https://www.mansor.com.uy/wp-content/uploads/2020/06/imagen-no-disponible2.jpg'">
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
