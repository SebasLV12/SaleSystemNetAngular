import { DetailSale } from "./detail-sale";

export interface Sale {
    idSale?:number,
    numberDoc?:string,
    paymentType:string,
    createdOn?:string,
    totalString:string,
    detailSales: DetailSale[]
}

