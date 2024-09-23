Create database QLHOA
go
use QLHOA
go
-- Tạo bảng Category
CREATE TABLE Categories (
    Id INT PRIMARY KEY,
    IDCate NVARCHAR(MAX),
    NameCate NVARCHAR(MAX)
);
CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    NamePro NVARCHAR(MAX),
    DecriptionPro NVARCHAR(MAX),
    CategoryId INT,
    Price DECIMAL,
    ImagePro NVARCHAR(MAX),
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);
-- Tạo bảng Customer
CREATE TABLE Customers (
    IDCus INT PRIMARY KEY,
    NameCus NVARCHAR(MAX),
    PhoneCus NVARCHAR(MAX),
    EmailCus NVARCHAR(MAX),
    PassCus NVARCHAR(MAX),
    UserName NVARCHAR(MAX)
);

-- Tạo bảng OrderPro
CREATE TABLE OrderPros (
    ID INT PRIMARY KEY,
    DateOrder DATETIME,
    IDCus INT,
    AddressDelivery NVARCHAR(MAX),
    FOREIGN KEY (IDCus) REFERENCES Customers(IDCus)
);

-- Tạo bảng OrderDetail
CREATE TABLE OrderDetails (
    ID INT PRIMARY KEY,
    IDProduct INT,
    IDOrder INT,
    Quantity INT,
    UnitPrice FLOAT,
    FOREIGN KEY (IDProduct) REFERENCES Products(ProductID),
    FOREIGN KEY (IDOrder) REFERENCES OrderPros(ID)
);
-- Tạo bảng Voucher
CREATE TABLE Vouchers (
    VoucherID INT PRIMARY KEY,
    IDProduct INT,
    Value DECIMAL,
    FOREIGN KEY (IDProduct) REFERENCES Products(ProductID)
);

-- Tạo bảng AdminUser
CREATE TABLE AdminUsers (
    ID INT PRIMARY KEY,
    NameUser NVARCHAR(MAX),
    RoleUser NVARCHAR(MAX),
    PasswordUser NVARCHAR(MAX)
);
-- Chèn dữ liệu mẫu cho bảng Categories
INSERT INTO Categories (Id, IDCate, NameCate) VALUES
(1, 'C001', 'Category 1'),
(2, 'C002', 'Category 2'),
(3, 'C003', 'Category 3');

-- Chèn dữ liệu mẫu cho bảng Customers
INSERT INTO Customers (IDCus, NameCus, PhoneCus, EmailCus, PassCus, UserName) VALUES
(1, 'Customer 1', '123456789', 'customer1@example.com', 'password1', 'user1'),
(2, 'Customer 2', '987654321', 'customer2@example.com', 'password2', 'user2'),
(3, 'Customer 3', '111222333', 'customer3@example.com', 'password3', 'user3');

-- Chèn dữ liệu mẫu cho bảng OrderPros
INSERT INTO OrderPros (ID, DateOrder, IDCus, AddressDelivery) VALUES
(1, '2023-01-01', 1, 'Address 1'),
(2, '2023-02-01', 2, 'Address 2'),
(3, '2023-03-01', 3, 'Address 3');
-- Chèn dữ liệu mẫu cho bảng Products
INSERT INTO Products (ProductID, NamePro, DecriptionPro, CategoryId, Price, ImagePro) VALUES
(1, 'Product 1', 'Description 1', 1, 9.99, 'image1.jpg'),
(2, 'Product 2', 'Description 2', 1, 14.99, 'image2.jpg'),
(3, 'Product 3', 'Description 3', 2, 19.99, 'image3.jpg');
-- Chèn dữ liệu mẫu cho bảng OrderDetails
INSERT INTO OrderDetails (ID, IDProduct, IDOrder, Quantity, UnitPrice) VALUES
(1, 1, 1, 5, 10.99),
(2, 2, 1, 3, 15.99),
(3, 3, 2, 2, 20.99);
-- Chèn dữ liệu mẫu cho bảng Vouchers
INSERT INTO Vouchers (VoucherID, IDProduct, Value) VALUES
(1, 1, 5),
(2, 2, 10),
(3, 3, 15);

-- Chèn dữ liệu mẫu cho bảng AdminUsers
INSERT INTO AdminUsers (ID, NameUser, RoleUser, PasswordUser) VALUES
(1, 'Admin 1', 'Role 1', 'admin1password'),
(2, 'Admin 2', 'Role 2', 'admin2password'),
(3, 'Admin 3', 'Role 3', 'admin3password');

GO


-- Tạo Trigger cho Bảng Products
CREATE TRIGGER trg_Products_Insert
ON Products
AFTER INSERT
AS
BEGIN
    -- Thêm một bản ghi tương ứng vào bảng Vouchers với giá trị mặc định là 0
    INSERT INTO Vouchers (IDProduct, Value)
    SELECT ProductID, 0
    FROM inserted;
END;
--thuc thi trigger
-- Thêm một sản phẩm mới vào bảng Products
INSERT INTO Products (ProductID, NamePro, DecriptionPro, CategoryId, Price, ImagePro)
VALUES (4, 'Product 4', 'Description 4', 3, 24.99, 'image4.jpg');

GO

-- Tạo Trigger cho Bảng Products khi xóa
CREATE TRIGGER trg_Products_Delete
ON Products
AFTER DELETE
AS
BEGIN
    -- Xóa các đơn đặt hàng liên quan trong bảng OrderDetails
    DELETE FROM OrderDetails
    WHERE IDProduct IN (SELECT ProductID FROM deleted);
END;
-- Xóa một sản phẩm từ bảng Products
DELETE FROM Products
WHERE ProductID = 4; -- Đặt ID sản phẩm bạn muốn xóa

GO

-- Tạo trigger cập nhật sản phẩm khi chi tiết đơn hàng được cập nhật
CREATE TRIGGER UpdateProductOnOrderDetailUpdate
ON OrderDetails
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Cập nhật thông tin sản phẩm trong bảng Products
    UPDATE Products
    SET
        NamePro = p.NamePro,
        DecriptionPro = p.DecriptionPro,
        CategoryId = p.CategoryId,
        Price = p.Price,
        ImagePro = p.ImagePro
    FROM
        Products p
    INNER JOIN
        inserted i ON p.ProductID = i.IDProduct;

END;
--thuc thi trigger
-- Cập nhật một chi tiết đơn hàng trong bảng OrderDetails
UPDATE OrderDetails
SET Quantity = 10
WHERE ID = 1; -- Đặt ID chi tiết đơn hàng bạn muốn cập nhật

select *from Categories
select *from Products
select *from Customers
select *from OrderPros
select *from OrderDetails
select *from Vouchers
select *from AdminUsers

GO
-- Hàm để lấy tổng số lượng các danh mục
CREATE FUNCTION dbo.GetCategoryCount()
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;
    SELECT @Count = COUNT(*) FROM Categories;
    RETURN @Count;
END;
GO

-- Hàm để lấy tên danh mục theo ID
CREATE FUNCTION dbo.GetCategoryNameById(@CategoryId INT)
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @CategoryName NVARCHAR(MAX);
    SELECT @CategoryName = NameCate FROM Categories WHERE Id = @CategoryId;
    RETURN @CategoryName;
END;
GO

-- Hàm để lấy số lượng sản phẩm trong một danh mục cụ thể
CREATE FUNCTION dbo.GetProductCountByCategory(@CategoryId INT)
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;
    SELECT @Count = COUNT(*) FROM Products WHERE CategoryId = @CategoryId;
    RETURN @Count;
END;
GO

-- Hàm để lấy giá sản phẩm theo ID
CREATE FUNCTION dbo.GetProductPriceById(@ProductId INT)
RETURNS DECIMAL
AS
BEGIN
    DECLARE @ProductPrice DECIMAL;
    SELECT @ProductPrice = Price FROM Products WHERE ProductID = @ProductId;
    RETURN @ProductPrice;
END;
GO

-- Hàm để lấy thông tin khách hàng theo tên đăng nhập
CREATE FUNCTION dbo.GetCustomerByUsername(@Username NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN
    SELECT * FROM Customers WHERE UserName = @Username;

GO
-- Hàm để lấy số lượng đơn hàng theo ID khách hàng
CREATE FUNCTION dbo.GetOrderCountByCustomerId(@CustomerId INT)
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;
    SELECT @Count = COUNT(*) FROM OrderPros WHERE IDCus = @CustomerId;
    RETURN @Count;
END;
GO

-- Hàm để tính tổng giá cho một đơn hàng
CREATE FUNCTION dbo.CalculateOrderTotal(@OrderId INT)
RETURNS DECIMAL
AS
BEGIN
    DECLARE @Total DECIMAL;
    SELECT @Total = SUM(Quantity * UnitPrice) FROM OrderDetails WHERE IDOrder = @OrderId;
    RETURN @Total;
END;
GO

-- Hàm để lấy giá trị voucher theo ID sản phẩm
CREATE FUNCTION dbo.GetVoucherValueByProductId(@ProductId INT)
RETURNS DECIMAL
AS
BEGIN
    DECLARE @Value DECIMAL;
    SELECT @Value = Value FROM Vouchers WHERE IDProduct = @ProductId;
    RETURN @Value;
END;
GO

---Procedure cho Bảng Categories:
-- Procedure để lấy tổng số lượng các danh mục
CREATE PROCEDURE dbo.GetCategoryCountProcedure
AS
BEGIN
    SELECT COUNT(*) AS CategoryCount FROM Categories;
END;
GO
EXEC dbo.GetCategoryCountProcedure;

GO

-- Procedure để lấy tên danh mục theo ID
CREATE PROCEDURE dbo.GetCategoryNameByIdProcedure
    @CategoryId INT
AS
BEGIN
    SELECT NameCate FROM Categories WHERE Id = @CategoryId;
END;
GO
DECLARE @CategoryId INT = 1; -- Điều chỉnh ID tùy ý
EXEC dbo.GetCategoryNameByIdProcedure @CategoryId;

GO

----Procedure cho Bảng Products:

-- Procedure để lấy số lượng sản phẩm trong một danh mục cụ thể
CREATE PROCEDURE dbo.GetProductCountByCategoryProcedure
    @CategoryId INT
AS
BEGIN
    SELECT COUNT(*) AS ProductCount FROM Products WHERE CategoryId = @CategoryId;
END;
GO
DECLARE @CategoryId INT = 1; -- Điều chỉnh ID tùy ý
EXEC dbo.GetProductCountByCategoryProcedure @CategoryId;

GO

-- Procedure để lấy giá sản phẩm theo ID
CREATE PROCEDURE dbo.GetProductPriceByIdProcedure
    @ProductId INT
AS
BEGIN
    SELECT Price FROM Products WHERE ProductID = @ProductId;
END;
GO
DECLARE @ProductId INT = 1; -- Điều chỉnh ID tùy ý
EXEC dbo.GetProductPriceByIdProcedure @ProductId;

GO

---Procedure cho Bảng Customers:

-- Procedure để lấy thông tin khách hàng theo tên đăng nhập
CREATE PROCEDURE dbo.GetCustomerByUsernameProcedure
    @Username NVARCHAR(MAX)
AS
BEGIN
    SELECT * FROM Customers WHERE UserName = @Username;
END;
GO
DECLARE @Username NVARCHAR(MAX) = 'user1'; -- Điều chỉnh tên đăng nhập tùy ý
EXEC dbo.GetCustomerByUsernameProcedure @Username;

GO


---Procedure cho Bảng OrderPros:

-- Procedure để lấy số lượng đơn hàng theo ID khách hàng
CREATE PROCEDURE dbo.GetOrderCountByCustomerIdProcedure
    @CustomerId INT
AS
BEGIN
    SELECT COUNT(*) AS OrderCount FROM OrderPros WHERE IDCus = @CustomerId;
END;
GO
DECLARE @CustomerId INT = 1; -- Điều chỉnh ID khách hàng tùy ý
EXEC dbo.GetOrderCountByCustomerIdProcedure @CustomerId;

GO


----Procedure cho Bảng OrderDetails:

-- Procedure để tính tổng giá cho một đơn hàng
CREATE PROCEDURE dbo.CalculateOrderTotalProcedure
    @OrderId INT
AS
BEGIN
    SELECT SUM(Quantity * UnitPrice) AS OrderTotal FROM OrderDetails WHERE IDOrder = @OrderId;
END;
GO
DECLARE @OrderId INT = 1; -- Điều chỉnh ID đơn hàng tùy ý
EXEC dbo.CalculateOrderTotalProcedure @OrderId;

GO


---Procedure cho Bảng Vouchers:

-- Procedure để lấy giá trị voucher theo ID sản phẩm
CREATE PROCEDURE dbo.GetVoucherValueByProductIdProcedure
    @ProductId INT
AS
BEGIN
    SELECT Value FROM Vouchers WHERE IDProduct = @ProductId;
END;
GO
DECLARE @ProductId INT = 1; -- Điều chỉnh ID sản phẩm tùy ý
EXEC dbo.GetVoucherValueByProductIdProcedure @ProductId;


--Procedure Thêm Danh Mục:
GO
CREATE PROCEDURE dbo.AddCategoryProcedure
    @Id INT,
    @IDCate NVARCHAR(MAX),
    @NameCate NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Categories (Id, IDCate, NameCate)
    VALUES (@Id, @IDCate, @NameCate);
END;
-- Thêm một danh mục mới
EXEC dbo.AddCategoryProcedure 
    @Id = 4,
    @IDCate = 'C004',
    @NameCate = 'Category 4';

GO

---Procedure Sửa Danh Mục:

CREATE PROCEDURE dbo.UpdateCategoryProcedure
    @Id INT,
    @IDCate NVARCHAR(MAX),
    @NameCate NVARCHAR(MAX)
AS
BEGIN
    UPDATE Categories
    SET IDCate = @IDCate, NameCate = @NameCate
    WHERE Id = @Id;
END;
-- Sửa thông tin một danh mục
EXEC dbo.UpdateCategoryProcedure 
    @Id = 4,
    @IDCate = 'C004_updated',
    @NameCate = 'Updated Category 4';

GO

---Procedure Xóa Danh Mục:

CREATE PROCEDURE dbo.DeleteCategoryProcedure
    @Id INT
AS
BEGIN
    DELETE FROM Categories
    WHERE Id = @Id;
END;
-- Xóa một danh mục
EXEC dbo.DeleteCategoryProcedure 
    @Id = 4;

GO

----Procedure Thêm Sản Phẩm:

CREATE PROCEDURE dbo.AddProductProcedure
    @ProductID INT,
    @NamePro NVARCHAR(MAX),
    @DecriptionPro NVARCHAR(MAX),
    @CategoryId INT,
    @Price DECIMAL,
    @ImagePro NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Products (ProductID, NamePro, DecriptionPro, CategoryId, Price, ImagePro)
    VALUES (@ProductID, @NamePro, @DecriptionPro, @CategoryId, @Price, @ImagePro);
END;
-- Thêm một sản phẩm mới
EXEC dbo.AddProductProcedure 
    @ProductID = 4,
    @NamePro = 'New Product',
    @DecriptionPro = 'Description for the new product',
    @CategoryId = 1,
    @Price = 29.99,
    @ImagePro = 'new_product_image.jpg';

GO

--Procedure Sửa Sản Phẩm:

CREATE PROCEDURE dbo.UpdateProductProcedure
    @ProductID INT,
    @NamePro NVARCHAR(MAX),
    @DecriptionPro NVARCHAR(MAX),
    @CategoryId INT,
    @Price DECIMAL,
    @ImagePro NVARCHAR(MAX)
AS
BEGIN
    UPDATE Products
    SET NamePro = @NamePro, DecriptionPro = @DecriptionPro, CategoryId = @CategoryId, Price = @Price, ImagePro = @ImagePro
    WHERE ProductID = @ProductID;
END;
-- Sửa thông tin một sản phẩm
EXEC dbo.UpdateProductProcedure 
    @ProductID = 4,
    @NamePro = 'Updated Product',
    @DecriptionPro = 'Updated description for the product',
    @CategoryId = 2,
    @Price = 39.99,
    @ImagePro = 'updated_product_image.jpg';

GO

----Procedure Xóa Sản Phẩm:

CREATE PROCEDURE dbo.DeleteProductProcedure
    @ProductID INT
AS
BEGIN
    DELETE FROM Products
    WHERE ProductID = @ProductID;
END;
-- Xóa một sản phẩm
EXEC dbo.DeleteProductProcedure 
    @ProductID = 4;

