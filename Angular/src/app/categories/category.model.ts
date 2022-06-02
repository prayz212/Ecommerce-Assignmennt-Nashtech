export interface ICategoryListResponse {
  categories: Category[];
  total: number;
  current: number;
}

export class Category {
  id: string = "";
  name: string = "";
  displayName: string = "";
  description?: string;

  static formatData(rawData: any): Category {
    const category = new Category();

    category.id = rawData.id || "0";
    category.name = rawData.name || "";
    category.displayName = rawData.displayName || "";

    if (rawData.description)
      category.description = rawData.description;

    return category;
  }
}
