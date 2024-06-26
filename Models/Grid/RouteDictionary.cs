﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace IP_AmazonFreshIndia_Project.Models
{
	public static class FilterPrefix
	{
		public const string Warehouse = "warehouse-";
		public const string Price = "price-";
		public const string Category = "category-";
		public const string Vendor = "vendor-";
	}

	public class RouteDictionary : Dictionary<string, string>
	{
		public int PageNumber
		{
			get => Get(nameof(GridDTO.PageNumber)).ToInt();
			set => this[nameof(GridDTO.PageNumber)] = value.ToString();
		}

		public int PageSize
		{
			get => Get(nameof(GridDTO.PageSize)).ToInt();
			set => this[nameof(GridDTO.PageSize)] = value.ToString();
		}

		public string SortField
		{
			get => Get(nameof(GridDTO.SortField));
			set => this[nameof(GridDTO.SortField)] = value;
		}

		public string SortDirection
		{
			get => Get(nameof(GridDTO.SortDirection));
			set => this[nameof(GridDTO.SortDirection)] = value;
		}

		public void SetSortAndDirection(string fieldName, RouteDictionary current)
		{
			this[nameof(GridDTO.SortField)] = fieldName;

			if (current.SortField.EqualsNoCase(fieldName) &&
				current.SortDirection == "asc")
				this[nameof(GridDTO.SortDirection)] = "desc";
			else
				this[nameof(GridDTO.SortDirection)] = "asc";
		}

		public string WarehouseFilter
		{
			get => Get(nameof(ProductsGridDTO.Warehouse))?.Replace(FilterPrefix.Warehouse, "");
			set => this[nameof(ProductsGridDTO.Warehouse)] = value;
		}

		public string VendorFilter
		{
			get => Get(nameof(ProductsGridDTO.Vendor))?.Replace(FilterPrefix.Vendor, "");
			set => this[nameof(ProductsGridDTO.Vendor)] = value;
		}


		public string PriceFilter
		{
			get => Get(nameof(ProductsGridDTO.Price))?.Replace(FilterPrefix.Price, "");
			set => this[nameof(ProductsGridDTO.Price)] = value;
		}

		public string CategoryFilter
		{
			get
			{
				string s = Get(nameof(ProductsGridDTO.Category))?.Replace(FilterPrefix.Category, "");
				int index = s?.IndexOf('-') ?? -1;
				return (index == -1) ? s : s.Substring(0, index);
			}
			set => this[nameof(ProductsGridDTO.Category)] = value;
		}

		public void ClearFilters() =>
			WarehouseFilter = PriceFilter = CategoryFilter = ProductsGridDTO.DefaultFilter;

		private string Get(string key) => Keys.Contains(key) ? this[key] : null;

		public RouteDictionary Clone()
		{
			var clone = new RouteDictionary();
			foreach (var key in Keys)
			{
				clone.Add(key, this[key]);
			}
			return clone;
		}
	}
}
