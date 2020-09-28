import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  //id: yup.string().required(),
  stock: yup.number().required(),
});

export default schema;
