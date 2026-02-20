<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="prjLibrarySystem.Books" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book Management - Library System</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css?v=2.0" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css?v=2.0" rel="stylesheet">
    <style>
        .sidebar {
            min-height: 100vh;
            background: linear-gradient(135deg, #8b0000 0%, #b11226 100%);
        }
        .sidebar .nav-link {
            color: white;
            padding: 15px 20px;
            border-radius: 0;
        }
        .sidebar .nav-link:hover {
            background-color: rgba(255, 255, 255, 0.1);
            color: white;
        }
        .sidebar .nav-link.active {
            background: rgba(255, 255, 255, 0.2);
            border-left: 4px solid white;
        }
        .main-content {
            padding: 20px;
        }
        .action-buttons .btn {
            margin-right: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <!-- Sidebar -->
                <nav class="col-md-3 col-lg-2 d-md-block sidebar collapse">
                    <div class="position-sticky pt-3">
                        <h4 class="text-white text-center mb-4">Library System</h4>
                        <ul class="nav flex-column">
                            <li class="nav-item">
                                <a class="nav-link" href="WebForm1.aspx">
                                    <i class="fas fa-tachometer-alt me-2"></i> Dashboard
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link active" href="Books.aspx">
                                    <i class="fas fa-book me-2"></i> Books
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="Members.aspx">
                                    <i class="fas fa-users me-2"></i> Members
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="borrowtransac.aspx">
                                    <i class="fas fa-hand-holding me-2"></i> Borrow Transaction
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="reports.aspx">
                                    <i class="fas fa-chart-bar me-2"></i> Reports
                                </a>
                            </li>
                        </ul>
                    </div>
                </nav>

                <!-- Main Content -->
                <main class="col-md-9 ms-sm-auto col-lg-10 px-md-4 main-content">
                    <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                        <h1 class="h2">Book Management</h1>
                        <div class="btn-toolbar mb-2 mb-md-0">
                            <div class="btn-group me-2">
                                <asp:Button ID="btnAddBook" runat="server" Text="Add New Book" 
                                    CssClass="btn btn-primary" OnClick="btnAddBook_Click" />
                            </div>
                        </div>
                    </div>

                    <!-- Search and Filter -->
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <div class="input-group">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" 
                                    placeholder="Search by title, author, or ISBN..."></asp:TextBox>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" 
                                    CssClass="btn btn-outline-secondary" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select" 
                                AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                <asp:ListItem Value="">All Categories</asp:ListItem>
                                <asp:ListItem Value="Fiction">Fiction</asp:ListItem>
                                <asp:ListItem Value="Non-Fiction">Non-Fiction</asp:ListItem>
                                <asp:ListItem Value="Science">Science</asp:ListItem>
                                <asp:ListItem Value="Technology">Technology</asp:ListItem>
                                <asp:ListItem Value="History">History</asp:ListItem>
                                <asp:ListItem Value="Biography">Biography</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlAvailability" runat="server" CssClass="form-select" 
                                AutoPostBack="true" OnSelectedIndexChanged="ddlAvailability_SelectedIndexChanged">
                                <asp:ListItem Value="">All Books</asp:ListItem>
                                <asp:ListItem Value="Available">Available</asp:ListItem>
                                <asp:ListItem Value="Borrowed">Borrowed</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- Books Grid -->
                    <div class="card shadow">
                        <div class="card-header py-3">
                            <h6 class="m-0 font-weight-bold text-primary">Books Inventory</h6>
                        </div>
                        <div class="card-body">
                            <asp:GridView ID="gvBooks" runat="server" CssClass="table table-hover" 
                                AutoGenerateColumns="false" GridLines="None" AllowPaging="true" 
                                PageSize="10" OnPageIndexChanging="gvBooks_PageIndexChanging"
                                OnRowCommand="gvBooks_RowCommand" DataKeyNames="BookId">
                                <Columns>
                                    <asp:BoundField DataField="BookId" HeaderText="ID" HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" />
                                    <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
                                    <asp:BoundField DataField="Title" HeaderText="Title" />
                                    <asp:BoundField DataField="Author" HeaderText="Author" />
                                    <asp:BoundField DataField="Category" HeaderText="Category" />
                                    <asp:BoundField DataField="TotalCopies" HeaderText="Total Copies" />
                                    <asp:BoundField DataField="AvailableCopies" HeaderText="Available" />
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <span class='badge <%# Eval("AvailableCopies").ToString() == "0" ? "bg-danger" : "bg-success" %>'>
                                                <%# Eval("AvailableCopies").ToString() == "0" ? "Out of Stock" : "Available" %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <div class="action-buttons">
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditBook" 
                                                    CommandArgument='<%# Eval("BookId") %>' CssClass="btn btn-sm btn-warning">
                                                    <i class="fas fa-edit"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteBook" 
                                                    CommandArgument='<%# Eval("BookId") %>' CssClass="btn btn-sm btn-danger"
                                                    OnClientClick="return confirm('Are you sure you want to delete this book?');">
                                                    <i class="fas fa-trash"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnDetails" runat="server" CommandName="ViewDetails" 
                                                    CommandArgument='<%# Eval("BookId") %>' CssClass="btn btn-sm btn-info">
                                                    <i class="fas fa-eye"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pagination" />
                            </asp:GridView>
                        </div>
                    </div>
                </main>
            </div>
        </div>

        <!-- Add/Edit Book Modal -->
        <div class="modal fade" id="bookModal" tabindex="-1" aria-labelledby="bookModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="bookModalLabel">
                            <asp:Label ID="lblModalTitle" runat="server" Text="Add New Book"></asp:Label>
                        </h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label for="txtISBN" class="form-label">ISBN</label>
                                <asp:TextBox ID="txtISBN" runat="server" CssClass="form-control" required></asp:TextBox>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="txtTitle" class="form-label">Title</label>
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label for="txtAuthor" class="form-label">Author</label>
                                <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control" required></asp:TextBox>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="txtPublisher" class="form-label">Publisher</label>
                                <asp:TextBox ID="txtPublisher" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 mb-3">
                                <label for="txtPublicationYear" class="form-label">Publication Year</label>
                                <asp:TextBox ID="txtPublicationYear" runat="server" CssClass="form-control" 
                                    TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-4 mb-3">
                                <label for="ddlBookCategory" class="form-label">Category</label>
                                <asp:DropDownList ID="ddlBookCategory" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="">Select Category</asp:ListItem>
                                    <asp:ListItem Value="Fiction">Fiction</asp:ListItem>
                                    <asp:ListItem Value="Non-Fiction">Non-Fiction</asp:ListItem>
                                    <asp:ListItem Value="Science">Science</asp:ListItem>
                                    <asp:ListItem Value="Technology">Technology</asp:ListItem>
                                    <asp:ListItem Value="History">History</asp:ListItem>
                                    <asp:ListItem Value="Biography">Biography</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 mb-3">
                                <label for="txtPrice" class="form-label">Price</label>
                                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" 
                                    TextMode="Number" step="0.01"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label for="txtTotalCopies" class="form-label">Total Copies</label>
                                <asp:TextBox ID="txtTotalCopies" runat="server" CssClass="form-control" 
                                    TextMode="Number" min="1"></asp:TextBox>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="txtDescription" class="form-label">Description</label>
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" 
                                    TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfBookId" runat="server" />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <asp:Button ID="btnSaveBook" runat="server" Text="Save Book" 
                            CssClass="btn btn-primary" OnClick="btnSaveBook_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script>
        function showBookModal() {
            var modal = new bootstrap.Modal(document.getElementById('bookModal'));
            modal.show();
        }

        function hideBookModal() {
            var modal = bootstrap.Modal.getInstance(document.getElementById('bookModal'));
            if (modal) {
                modal.hide();
            }
        }
    </script>
</body>
</html>
