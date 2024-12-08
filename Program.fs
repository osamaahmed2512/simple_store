﻿open System
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
    

 // Product details display
    let productDetailsLabel = new Label(Text = "Select a product to view details.", TextAlign = Drawing.ContentAlignment.MiddleCenter, Font = new Drawing.Font("Arial", 12.0F))
    productDetailsLabel.AutoSize <- true  // Allow the label to resize based on content   

// Create a panel for the product details label
    let productDetailsPanel = new Panel(Dock = DockStyle.Fill)
    productDetailsPanel.Controls.Add(productDetailsLabel)   

   
    productCatalog 
    |> List.iter (fun p -> productListBox.Items.Add($"{p.Name} - ${p.Price}") |> ignore)


   



//Create SplitContainer to manage layout (upper, down)
    let splitContainer = new SplitContainer(Dock = DockStyle.Fill, Orientation = Orientation.Horizontal)
    splitContainer.Panel1.Controls.Add(productListBox)  // Product list on the top
    splitContainer.Panel2.Controls.Add(productDetailsPanel)  // Product details in the middle

    form.Controls.Add(splitContainer)
    
   

    form

// Main program
[<EntryPoint>]
let main argv =
    Application.EnableVisualStyles()
    let productCatalog = loadProductCatalog "C:/Users/DELL/source/repos/simplestoresimulator/simplestoresimulator/products.json"
    let mainForm = createMainForm productCatalog
    Application.Run(mainForm)   
    0

