using System.ComponentModel.DataAnnotations;

namespace AMS_API.Models
{
    public class assets
    {
        public int id_asset { get; set; }
        [Required(ErrorMessage = "Serial is required")]
        public string serial { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string asset_name { get; set; }
        public string asset_desc { get; set; }
        public int? id_type { get; set; }
        public int? id_user { get; set; }
        public DateTime? purchase_date { get; set; }
        public decimal? purchase_cost { get; set; }
        public bool depreciate { get; set; }
        public bool requestable { get; set; }
        public bool consumable { get; set; }
        public int? id_company { get; set; }
        public string status { get; set; }
        public DateTime? warranty_expiration { get; set; }

    }

    public class update_asset
    {
        public int id_asset { get; set; }
        public string asset_name { get; set; }
        public string asset_desc { get; set; }
        public int? id_type { get; set; }
        public int? id_user { get; set; }
        public DateTime? purchase_date { get; set; }
        public decimal? purchase_cost { get; set; }
        public bool depreciate { get; set; }
        public bool requestable { get; set; }
        public bool consumable { get; set; }
        public int? id_company { get; set; }
        public string status { get; set; }
        public DateTime? warranty_expiration { get; set; }
        public locations location { get; set; }
        public SelectOrAddCompany company { get; set; }
    }

    public class new_asset
    {
        public assets asset { get; set; }
        public locations location { get; set; }
        public SelectOrAddCompany company { get; set; }
    }

    public class update_asset_warranty
    {
        [Required(ErrorMessage = "Id is required")]
        public int id_asset { get; set; }
        [Required(ErrorMessage = "warranty expiration is required")]
        public DateTime? warranty_expiration { get; set; }
    }

    public class asset_type
    {
        public int id_type { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string name_type { get; set; }
        public string desc_type { get; set; }
        public int id_user { get; set; }
    }
    public class activation_asset
    {
        [Required(ErrorMessage = "id is required")]
        public int id_asset { get; set; }
        [Required(ErrorMessage = "activation is required")]
        public bool activation { get; set; }
    }
    public class add_asset_type
    {
        [Required(ErrorMessage = "Name is required")]
        public string name_type { get; set; }
        public string desc_type { get; set; }
    }


    public class asset_maintenance
    {
        public int id_maintenance { get; set; }
        [Required(ErrorMessage = "Asset id is required")]
        public int id_asset { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string? title { get; set; }
        public string? maintenance_desc { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? completion_date { get; set; }
        public int? maintenance_time { get; set; }
        public decimal? cost { get; set; }
        public string? notes { get; set; }
    }
    public class add_maintenance
    {
        public asset_maintenance maintenance { get; set; }
        public locations location { get; set; }
    }
    public class update_maintenance
    {
        [Required(ErrorMessage = "Maintenance id is required")]
        public int id_maintenance { get; set; }
        public int id_asset { get; set; }
        public string? title { get; set; }
        public string? maintenance_desc { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? completion_date { get; set; }
        public int? maintenance_time { get; set; }
        public decimal? cost { get; set; }
        public string? notes { get; set; }
        public locations location { get; set; }
    }

    public class add_request_asset
    {
        [Required(ErrorMessage = "Asset id is required")]
        public int id_asset { get; set; }
        public int? id_user { get; set; }
        public DateTime? requested_at { get; set; }
        public string? notes { get; set; }
        public SelectOrAddCompany company { get; set; }
    }

    public class update_request_asset
    {
        [Required(ErrorMessage = "request id is required")]
        public int id_request { get; set; }
        public int id_asset { get; set; }
        public int? id_user { get; set; }
        public DateTime? requested_at { get; set; }
        public DateTime? denied_at { get; set; }
        public string? notes { get; set; }
        public SelectOrAddCompany company { get; set; }
    }

    public class accept_request_asset
    {

        [Required(ErrorMessage = "request id is required")]
        public int id_request { get; set; }
        public bool accept { get; set; }

    }

    public class add_consumable_asset
    {
        [Required(ErrorMessage = "Asset id is required")]
        public int id_asset { get; set; }
        public int? id_user { get; set; }
        public string? notes { get; set; }
        public DateTime? purchase_date { get; set; }
        public decimal? purchase_cost { get; set; }
        public SelectOrAddCompany company { get; set; }
    }
    public class update_consumable_asset
    {
        public int id_usage { get; set; }
        [Required(ErrorMessage = "Asset id is required")]
        public int id_asset { get; set; }
        public int? id_user { get; set; }
        public string? notes { get; set; }
        public DateTime? purchase_date { get; set; }
        public decimal? purchase_cost { get; set; }
        public SelectOrAddCompany company { get; set; }
    }
}
