import React from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button } from "reactstrap";
import InputField from "../../../../components/inputs/InputField";
import Validator from "../../../../common/helpers/YupValidator";
import schema from "../validation";

const StockForm = (props) => {
  const { handleSubmit, handleCancel } = props;
  return (
    <Form onSubmit={handleSubmit} className="addForm">
      <Field label="stock" name="stock" component={InputField} type="number" />
      <Button className="product-form__button" color="primary" type="submit">
        Disminuir
      </Button>
      <Button
        className="product-form__button"
        color="secondary"
        type="button"
        onClick={handleCancel}
      >
        Cancelar
      </Button>
    </Form>
  );
};

StockForm.propTypes = {
  handleSubmit: PropTypes.func.isRequired,
  handleCancel: PropTypes.func.isRequired,
};

export default reduxForm({
  form: "stock",
  validate: Validator(schema),
  enableReinitialize: true,
})(StockForm);
