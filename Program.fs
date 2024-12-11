open System
open System.IO
open System.Windows.Forms
open System.Text.Json

 
type Product = { Name: string; Price: decimal; Description: string }

// Function to load the product catalog from a JSON file
let loadProductCatalog (filePath: string): Product list =
    if File.Exists(filePath) then
        let json = File.ReadAllText(filePath)
        JsonSerializer.Deserialize<Product list>(json)
    else
        MessageBox.Show($"Error: File {filePath} not found.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
        []


// Create and initialize the main form
let createMainForm (productCatalog: Product list) =

    // Form initialization
    let form = new Form(Text = "Simple Store Simulator", Width = 800, Height = 600)
    
  
    // Product catalog display
    let productListBox = new ListBox(Dock = DockStyle.Fill)
    let cartListBox = new ListBox(Dock = DockStyle.Fill)  

 // Product details display
    let productDetailsLabel = new Label(Text = "Select a product to view details.", TextAlign = Drawing.ContentAlignment.MiddleCenter, Font = new Drawing.Font("Arial", 12.0F))
    productDetailsLabel.AutoSize <- true  // Allow the label to resize based on content   

// Create a panel for the product details label
    let productDetailsPanel = new Panel(Dock = DockStyle.Fill)
    productDetailsPanel.Controls.Add(productDetailsLabel)   
    let addToCartButton = new Button(Text = "Add to Cart", Dock = DockStyle.Top, Height = 40)
    let removeFromCartButton = new Button(Text = "Remove from Cart", Dock = DockStyle.Top, Height = 40)
    let checkoutButton = new Button(Text = "Checkout", Dock = DockStyle.Top, Height = 40)
   
    productCatalog 
    |> List.iter (fun p -> productListBox.Items.Add($"{p.Name} - ${p.Price}") |> ignore)


   



//Create SplitContainer to manage layout (upper, down)
    let splitContainer = new SplitContainer(Dock = DockStyle.Fill, Orientation = Orientation.Horizontal)
    splitContainer.Panel1.Controls.Add(productListBox)  // Product list on the top
    splitContainer.Panel2.Controls.Add(productDetailsPanel)  // Product details in the middle

    form.Controls.Add(splitContainer)
    
    let rightPanel = new Panel(Dock = DockStyle.Right, Width = 320)
    rightPanel.Controls.Add(cartListBox)
    rightPanel.Controls.Add(checkoutButton)
    rightPanel.Controls.Add(removeFromCartButton)
    rightPanel.Controls.Add(addToCartButton)
    form.Controls.Add(rightPanel)

        
    let updateProductDetails (selectedProduct: Product) =
        // Updating the product details label with selected product details
        productDetailsLabel.Text <- $"Name: {selectedProduct.Name}\nPrice: ${selectedProduct.Price}\nDescription: {selectedProduct.Description}"
   
    
    // Update product details when a product is selected
    productListBox.SelectedIndexChanged.Add(fun _ ->
        if productListBox.SelectedIndex >= 0 then
            let selectedProductName = productListBox.SelectedItem.ToString().Split(" - ").[0]
            match productCatalog |> List.tryFind (fun p -> p.Name = selectedProductName) with
    
            | Some product -> updateProductDetails product
            | None -> productDetailsLabel.Text <- "Product details not found."
    )

    form

// Main program
[<EntryPoint>]
let main argv =
    Application.EnableVisualStyles()
    let productCatalog = loadProductCatalog "C:/Users/DELL/source/repos/simplestoresimulator/simplestoresimulator/products.json"
    let mainForm = createMainForm productCatalog
    Application.Run(mainForm)   
    0

