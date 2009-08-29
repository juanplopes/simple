// <autogenerated>
//   This file was generated by T4 code generator DataClasses1.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

namespace Simple.Generator
{
    using System;
    using System.ComponentModel;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;

    [Table(Name = "dbo.Products")]
    public partial class Product : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private int productID;
        private string productName;
        private int? supplierID;
        private int? categoryID;
        private string quantityPerUnit;
        private decimal? unitPrice;
        private short? unitsInStock;
        private short? unitsOnOrder;
        private short? reorderLevel;
        private bool discontinued;
        private EntitySet<Order_Detail> order_Details;
        private EntityRef<Category> category;
        private EntityRef<Supplier> supplier;
        
        public Product()
        {
            this.order_Details = new EntitySet<Order_Detail>(this.AttachOrder_Details, this.DetachOrder_Details);
            this.category = default(EntityRef<Category>);
            this.supplier = default(EntityRef<Supplier>);
            this.OnCreated();
        }
        
        public event PropertyChangingEventHandler PropertyChanging;
        
        public event PropertyChangedEventHandler PropertyChanged;

        [Column(Name = "ProductID", Storage = "productID", CanBeNull = false, DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int ProductID
        {
            get
            {
                return this.productID;
            }
        
            set
            {
                if (this.productID != value)
                {
                    this.OnProductIDChanging(value);
                    this.SendPropertyChanging("ProductID");
                    this.productID = value;
                    this.SendPropertyChanged("ProductID");
                    this.OnProductIDChanged();
                }
            }
        }

        [Column(Name = "ProductName", Storage = "productName", CanBeNull = false, DbType = "NVarChar(40) NOT NULL")]
        public string ProductName
        {
            get
            {
                return this.productName;
            }
        
            set
            {
                if (this.productName != value)
                {
                    this.OnProductNameChanging(value);
                    this.SendPropertyChanging("ProductName");
                    this.productName = value;
                    this.SendPropertyChanged("ProductName");
                    this.OnProductNameChanged();
                }
            }
        }

        [Column(Name = "SupplierID", Storage = "supplierID", CanBeNull = true, DbType = "Int")]
        public int? SupplierID
        {
            get
            {
                return this.supplierID;
            }
        
            set
            {
                if (this.supplierID != value)
                {
                    if (this.supplier.HasLoadedOrAssignedValue)
                    {
                        throw new ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnSupplierIDChanging(value);
                    this.SendPropertyChanging("SupplierID");
                    this.supplierID = value;
                    this.SendPropertyChanged("SupplierID");
                    this.OnSupplierIDChanged();
                }
            }
        }

        [Column(Name = "CategoryID", Storage = "categoryID", CanBeNull = true, DbType = "Int")]
        public int? CategoryID
        {
            get
            {
                return this.categoryID;
            }
        
            set
            {
                if (this.categoryID != value)
                {
                    if (this.category.HasLoadedOrAssignedValue)
                    {
                        throw new ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnCategoryIDChanging(value);
                    this.SendPropertyChanging("CategoryID");
                    this.categoryID = value;
                    this.SendPropertyChanged("CategoryID");
                    this.OnCategoryIDChanged();
                }
            }
        }

        [Column(Name = "QuantityPerUnit", Storage = "quantityPerUnit", CanBeNull = true, DbType = "NVarChar(20)")]
        public string QuantityPerUnit
        {
            get
            {
                return this.quantityPerUnit;
            }
        
            set
            {
                if (this.quantityPerUnit != value)
                {
                    this.OnQuantityPerUnitChanging(value);
                    this.SendPropertyChanging("QuantityPerUnit");
                    this.quantityPerUnit = value;
                    this.SendPropertyChanged("QuantityPerUnit");
                    this.OnQuantityPerUnitChanged();
                }
            }
        }

        [Column(Name = "UnitPrice", Storage = "unitPrice", CanBeNull = true, DbType = "Money")]
        public decimal? UnitPrice
        {
            get
            {
                return this.unitPrice;
            }
        
            set
            {
                if (this.unitPrice != value)
                {
                    this.OnUnitPriceChanging(value);
                    this.SendPropertyChanging("UnitPrice");
                    this.unitPrice = value;
                    this.SendPropertyChanged("UnitPrice");
                    this.OnUnitPriceChanged();
                }
            }
        }

        [Column(Name = "UnitsInStock", Storage = "unitsInStock", CanBeNull = true, DbType = "SmallInt")]
        public short? UnitsInStock
        {
            get
            {
                return this.unitsInStock;
            }
        
            set
            {
                if (this.unitsInStock != value)
                {
                    this.OnUnitsInStockChanging(value);
                    this.SendPropertyChanging("UnitsInStock");
                    this.unitsInStock = value;
                    this.SendPropertyChanged("UnitsInStock");
                    this.OnUnitsInStockChanged();
                }
            }
        }

        [Column(Name = "UnitsOnOrder", Storage = "unitsOnOrder", CanBeNull = true, DbType = "SmallInt")]
        public short? UnitsOnOrder
        {
            get
            {
                return this.unitsOnOrder;
            }
        
            set
            {
                if (this.unitsOnOrder != value)
                {
                    this.OnUnitsOnOrderChanging(value);
                    this.SendPropertyChanging("UnitsOnOrder");
                    this.unitsOnOrder = value;
                    this.SendPropertyChanged("UnitsOnOrder");
                    this.OnUnitsOnOrderChanged();
                }
            }
        }

        [Column(Name = "ReorderLevel", Storage = "reorderLevel", CanBeNull = true, DbType = "SmallInt")]
        public short? ReorderLevel
        {
            get
            {
                return this.reorderLevel;
            }
        
            set
            {
                if (this.reorderLevel != value)
                {
                    this.OnReorderLevelChanging(value);
                    this.SendPropertyChanging("ReorderLevel");
                    this.reorderLevel = value;
                    this.SendPropertyChanged("ReorderLevel");
                    this.OnReorderLevelChanged();
                }
            }
        }

        [Column(Name = "Discontinued", Storage = "discontinued", CanBeNull = false, DbType = "Bit NOT NULL")]
        public bool Discontinued
        {
            get
            {
                return this.discontinued;
            }
        
            set
            {
                if (this.discontinued != value)
                {
                    this.OnDiscontinuedChanging(value);
                    this.SendPropertyChanging("Discontinued");
                    this.discontinued = value;
                    this.SendPropertyChanged("Discontinued");
                    this.OnDiscontinuedChanged();
                }
            }
        }

        [Association(Name = "Product_Order_Detail", Storage = "order_Details", ThisKey = "ProductID", OtherKey = "ProductID")]
        public EntitySet<Order_Detail> Order_Details
        {
            get 
            {
                return this.order_Details; 
            }
        
            set 
            { 
                this.order_Details.Assign(value); 
            }
        }

        [Association(Name = "Category_Product", Storage = "category", ThisKey = "CategoryID", OtherKey = "CategoryID", IsForeignKey = true)]
        public Category Category
        {
            get
            {
                return this.category.Entity;
            }
        
            set
            {
                Category previousValue = this.category.Entity;
                if (previousValue != value || !this.category.HasLoadedOrAssignedValue)
                {
                    this.SendPropertyChanging("Category");
        
                    if (previousValue != null)
                    {
                        this.category.Entity = null;
                        previousValue.Products.Remove(this);
                    }
        
                    this.category.Entity = value;
        
                    if (value != null)
                    {
                        value.Products.Add(this);
                        this.categoryID = value.CategoryID;
                    }
                    else
                    {
                        this.categoryID = default(int?);
                    }
        
                    this.SendPropertyChanged("Category");
                }
            }
        }

        [Association(Name = "Supplier_Product", Storage = "supplier", ThisKey = "SupplierID", OtherKey = "SupplierID", IsForeignKey = true)]
        public Supplier Supplier
        {
            get
            {
                return this.supplier.Entity;
            }
        
            set
            {
                Supplier previousValue = this.supplier.Entity;
                if (previousValue != value || !this.supplier.HasLoadedOrAssignedValue)
                {
                    this.SendPropertyChanging("Supplier");
        
                    if (previousValue != null)
                    {
                        this.supplier.Entity = null;
                        previousValue.Products.Remove(this);
                    }
        
                    this.supplier.Entity = value;
        
                    if (value != null)
                    {
                        value.Products.Add(this);
                        this.supplierID = value.SupplierID;
                    }
                    else
                    {
                        this.supplierID = default(int?);
                    }
        
                    this.SendPropertyChanged("Supplier");
                }
            }
        }
        
        protected virtual void SendPropertyChanging(string propertyName)
        {
            if (this.PropertyChanging != null)
            {
                this.PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }
        
        protected virtual void SendPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        private void AttachOrder_Details(Order_Detail entity)
        {
            this.SendPropertyChanging("Order_Details");
            entity.Product = this;
        }
        
        private void DetachOrder_Details(Order_Detail entity)
        {
            this.SendPropertyChanging("Order_Details");
            entity.Product = null;
        }
        
        #region Extensibility methods
        
        partial void OnCreated();
        
        partial void OnLoaded();
        
        partial void OnValidate(ChangeAction action);
        
        partial void OnProductIDChanging(int value);
        
        partial void OnProductIDChanged();
        
        partial void OnProductNameChanging(string value);
        
        partial void OnProductNameChanged();
        
        partial void OnSupplierIDChanging(int? value);
        
        partial void OnSupplierIDChanged();
        
        partial void OnCategoryIDChanging(int? value);
        
        partial void OnCategoryIDChanged();
        
        partial void OnQuantityPerUnitChanging(string value);
        
        partial void OnQuantityPerUnitChanged();
        
        partial void OnUnitPriceChanging(decimal? value);
        
        partial void OnUnitPriceChanged();
        
        partial void OnUnitsInStockChanging(short? value);
        
        partial void OnUnitsInStockChanged();
        
        partial void OnUnitsOnOrderChanging(short? value);
        
        partial void OnUnitsOnOrderChanged();
        
        partial void OnReorderLevelChanging(short? value);
        
        partial void OnReorderLevelChanged();
        
        partial void OnDiscontinuedChanging(bool value);
        
        partial void OnDiscontinuedChanged();
        
        #endregion
    }
}