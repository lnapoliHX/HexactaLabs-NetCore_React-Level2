import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  name: yup.string().required(),
  costPrice: yup.number().required(),
  salePrice: yup.number().required(),
  productTypeId: yup
    .string()
    .required()
    .notOneOf(["default"], "Please select an option"),
  providerId: yup
    .string()
    .required()
    .notOneOf(["default"], "Please select an option"),
});

export default schema;
