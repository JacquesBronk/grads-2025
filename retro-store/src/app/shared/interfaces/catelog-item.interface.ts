export interface CatelogItem {
  id: string;
  sku: string;
  name: string;
  description: string;
  price: number;
  quantity: number;
  tags: string[];
  isDiscounted: boolean;
  discountPercentage: number;
  imageUrl: string;
}
