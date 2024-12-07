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

// Main program
[<EntryPoint>]
let main argv =
    Application.EnableVisualStyles()
    let productCatalog = loadProductCatalog "C:/Users/DELL/source/repos/simplestoresimulator/simplestoresimulator/products.json"
    Application.Run()   
    0

