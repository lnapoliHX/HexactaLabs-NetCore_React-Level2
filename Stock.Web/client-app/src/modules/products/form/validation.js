import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  name: yup
    .string()
    .required(),
  costPrice: yup
    .number()
    .positive()
    .min(1)
    .required(),
  salePrice: yup
    .number()
    .positive()
    .min(1)
    .required(),
  stock: yup
    .number()
    .integer()
    .positive()
    .min(1)
    .required(),
  providerId: yup
    .string()
    .required(),
  productTypeId: yup
    .string()
    .required()
});

export default schema;
