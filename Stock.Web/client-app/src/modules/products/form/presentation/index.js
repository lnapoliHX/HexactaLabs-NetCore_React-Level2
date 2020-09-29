import React from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button } from "reactstrap";
import Validator from "../../../../common/helpers/YupValidator";
import InputField from "../../../../components/inputs/InputField";
import schema from "../validation";

const ProviderForm = props => {
  const { handleSubmit, handleCancel } = props;
  return (
    <Form onSubmit={handleSubmit} className="addForm">
      <Field label="Nombre" name="Name" component={InputField} type="text" />
      <Field label="Precio Costo" name="CostPrice" component={InputField} type="text" />
      <Field label="Precio Venta" name="SalePrice" component={InputField} type="text" />
      <Field label="Stock" name="Stock" component={InputField} type="text" />
      <Field label="ProductTypeId" name="ProductTypeId" component={InputField} type="text" />
      <Field label="ProviderId" name="ProviderId" component={InputField} type="text" />
      <Button className="provider-form__button" color="primary" type="submit">
        Guardar
      </Button>
      <Button
        className="provider-form__button"
        color="secondary"
        type="button"
        onClick={handleCancel}
      >
        Cancelar
      </Button>
    </Form>
  );
};

ProviderForm.propTypes = {
  handleSubmit: PropTypes.func.isRequired,
  handleCancel: PropTypes.func.isRequired
};

export default reduxForm({
  form: "provider",
  validate: Validator(schema),
  enableReinitialize: true
})(ProviderForm);
