export class Product {
  productId: number | undefined
  name: string | undefined
  image: string | undefined
  description: string | undefined
  price: number | undefined
  productCategoryId: number | undefined
  sizeId: number | undefined
  crustId: number | undefined
  isActive: boolean | undefined
  quantity: number | undefined
  size: Size | undefined
  crust: Crust | undefined
  productCategory: Category | undefined
  pizzaToppings: Array<Topping> | undefined;
  pizzaSelectedToppingNames: string | undefined;
  isCustomized: boolean = false;
  priceAfterCustomization: number | undefined;
  total: number | undefined;
}

export class Category {
  categoryId: number | undefined;
  name: string | undefined;
  isActive: boolean | undefined;
}


export class Topping {
  toppingId: number | undefined
  name: string | undefined
  price: number | undefined
  isActive: boolean | undefined
}

export class Size {
  sizeId: number | undefined
  name: string | undefined
  price: number | undefined
  isActive: boolean | undefined
}

export class Crust {
  crustId: number | undefined
  name: string | undefined
  price: number | undefined
  isActive: boolean | undefined
}


export class ProductWrapper {
  productCategory: Category | undefined
  products: Array<Product> | undefined
}

export class Menu {
  products: Array<ProductWrapper> | undefined;

}

export class Order {
  customerNumber: string | undefined
  sessionId: string | undefined
  TotalPrice: number | undefined
  orderDateTime: Date | undefined
  deliverDateTime: Date | undefined
  status: number | undefined
  products: Array<Product> | undefined
}
