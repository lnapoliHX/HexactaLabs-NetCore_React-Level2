import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  name: yup.string().required(),
  costprice: yup.number().required(),
  saleprice: yup.number().required(),
  producttype: yup.string().required(),
  provider: yup.string().required(),
  _stock: yup.number().required(),
});

export default schema;
