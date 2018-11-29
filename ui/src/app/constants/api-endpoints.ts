const baseUrl = 'https://localhost:5001/api';

const productBase = baseUrl + '/products';
const categoryBase = baseUrl + '/categories';

export const ApiEndpoints = {
  Product: {
    GetPaged: productBase,
    GetSingle: productBase,
    DeleteSingle: productBase,
    UpdateSingle: productBase,
    CreateSingle: productBase,
  },
  Category: {
    GetPaged: categoryBase,
    GetImage: (categoryId) => `${categoryBase}/${categoryId}/image`,
    UpdateImage: (categoryId) => `${categoryBase}/${categoryId}/image`
  }
};
