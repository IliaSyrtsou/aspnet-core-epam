/**
 * Northwind API
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * Contact: ilia_syrtsou@epam.com
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */


export interface CreateProductModel {
    productName: string;
    supplierId: number;
    supplierName?: string;
    categoryId: number;
    categoryName?: string;
    quantityPerUnit: string;
    unitPrice: number;
    unitsInStock: number;
    unitsOnOrder?: number;
    reorderLevel?: number;
    discontinued?: boolean;
}
