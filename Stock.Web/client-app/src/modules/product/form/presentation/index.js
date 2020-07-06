import React from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button } from "reactstrap";
import Validator from "../../../../common/helpers/YupValidator";
import InputField from "../../../../components/inputs/InputField";
import schema from "../validation";

const productForm = props => {
  const { handleSubmit, handleCancel } = props;
  return (
    <Form onSubmit={handleSubmit}>
      <Field label="Nombre" name="name" component={InputField} type="text" />
      <Field label="Cost Price" name="costPrice" component={InputField} type="text" />
      <Field label="Sale Price" name="salePrice" component={InputField} type="text" />

      <Button className="product-form__button" color="primary" type="submit">
        Guardar
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

productForm.propTypes = {
  handleSubmit: PropTypes.func.isRequired,
  handleCancel: PropTypes.func.isRequired
};

export default reduxForm({
  form: "provider",
  validate: Validator(schema),
  enableReinitialize: true
})(productForm);
