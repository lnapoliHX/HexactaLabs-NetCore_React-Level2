import * as yup from "yup";
import "../../../common/helpers/YupConfig";

const schema = yup.object().shape({
  value: yup.number().positive().min(1),
});

export default schema;
