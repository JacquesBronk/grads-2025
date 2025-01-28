import { CatelogItem } from "./catelog-item.interface";

export interface Stock {
  items: CatelogItem[];
  totalCount: number,
  pageNumber: number,
  pageSize: number,
}
