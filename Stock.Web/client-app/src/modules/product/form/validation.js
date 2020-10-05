import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  name: yup.string().required(),
  costPrice: yup.number().required(),
  salePrice: yup.number().required(),
  productTypeId: yup.string().required(),
  productTypeDesc: yup.string().required()
});

export default schema;
