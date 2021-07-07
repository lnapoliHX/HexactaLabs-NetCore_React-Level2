import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  name: yup.string().required(),
  costprice: yup.string().required(),
  saleprice: yup.string().required(),
  productTypeId: yup.string().required()
});

export default schema;
